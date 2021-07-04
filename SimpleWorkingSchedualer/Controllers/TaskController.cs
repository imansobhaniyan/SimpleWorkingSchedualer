using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

using SimpleWorkingSchedualer.DataAccessLayer;
using SimpleWorkingSchedualer.Models.Common;
using SimpleWorkingSchedualer.Models.Task;
using SimpleWorkingSchedualer.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly SimpleWorkingSchedualerDbContext dbContext;

        private readonly DefaultHubClient defaultHubClient;

        public TaskController(SimpleWorkingSchedualerDbContext dbContext, DefaultHubClient defaultHubClient)
        {
            this.dbContext = dbContext;
            this.defaultHubClient = defaultHubClient;
        }

        [HttpGet]
        public async Task<ApiResult<List<UserTaskResult>>> Get()
        {
            try
            {
                var token = GetToken();

                var users = await dbContext.Users
                    .Include(f => f.UserTasks).ThenInclude(f => f.UserTaskStatusHistories)
                    .Where(f => dbContext.Users.Any(x => x.Role == StorageModels.User.UserRole.Admin && x.Token == token.ToString()) ? f.Token != token.ToString() : f.Token == token.ToString())
                    .ToListAsync();


                return new ApiResult<List<UserTaskResult>>(users.ConvertAll(user => new UserTaskResult(user)));
            }
            catch (Exception exception)
            {
                return new ApiResult<List<UserTaskResult>>(exception);
            }
        }

        [HttpPost]
        public async Task<ApiResult<TaskResult>> Post([FromBody] TaskModel model)
        {
            try
            {
                var user = await GetUserAsync();

                var task = model.Id == 0 ? new StorageModels.UserTask() : await dbContext.UserTasks.FirstOrDefaultAsync(f => f.Id == model.Id);

                task.Description = model.Description;
                task.Title = model.Title;
                task.TaskDate = model.Date.Date;
                task.User = user;

                if (task.Id == 0)
                {
                    await dbContext.UserTasks.AddAsync(task);
                }

                task.UserTaskStatusHistories.Add(new StorageModels.UserTaskStatusHistory { Status = StorageModels.UserTaskStatusHistory.TaskStatus.Pending });

                await dbContext.SaveChangesAsync();

                var result = new TaskResult(task);

                await defaultHubClient.AddOrUpdateTask(result, new UserTaskResult(user));

                return new ApiResult<TaskResult>(result);
            }
            catch (Exception exception)
            {
                return new ApiResult<TaskResult>(exception);
            }
        }



        [HttpPut]
        public async Task<ApiResult<TaskResult>> Put([FromBody] TaskModel model)
        {
            try
            {
                var user = await GetUserAsync();

                var userTask = await dbContext.UserTasks.FirstOrDefaultAsync(f => f.Id == model.Id);

                userTask.UserTaskStatusHistories.Add(new StorageModels.UserTaskStatusHistory { Status = (StorageModels.UserTaskStatusHistory.TaskStatus)model.Status });

                await dbContext.SaveChangesAsync();

                var result = new TaskResult(userTask);

                await defaultHubClient.UpdateStatus(result);

                return new ApiResult<TaskResult>(result);
            }
            catch (Exception exception)
            {
                return new ApiResult<TaskResult>(exception);
            }
        }

        private async Task<StorageModels.User> GetUserAsync()
        {
            StringValues token = GetToken();

            var user = await dbContext.Users
                .FirstOrDefaultAsync(f => f.Token == token.ToString());

            if (user == null)
                throw new Exception("invalid token");

            return user;
        }

        private StringValues GetToken()
        {
            var token = Request.Headers["token"];

            if (token == StringValues.Empty)
                throw new Exception("invalid token");

            return token;
        }
    }
}
