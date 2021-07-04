using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

using SimpleWorkingSchedualer.DataAccessLayer;
using SimpleWorkingSchedualer.Models.Common;
using SimpleWorkingSchedualer.Models.Task;

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

        public TaskController(SimpleWorkingSchedualerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ApiResult<List<TaskResult>>> Get()
        {
            try
            {
                var token = Request.Headers["token"];

                if (token == StringValues.Empty)
                    throw new Exception("invalid token");

                var user = await dbContext.Users
                    .Include(f => f.UserTasks).ThenInclude(f => f.UserTaskStatusHistories)
                    .FirstOrDefaultAsync(f => f.Token == token.ToString());

                if (user == null)
                    throw new Exception("invalid token");

                return new ApiResult<List<TaskResult>>(user.UserTasks.ConvertAll(task => new TaskResult(task)));
            }
            catch (Exception exception)
            {
                return new ApiResult<List<TaskResult>>(exception);
            }
        }

        [HttpPost]
        public async Task<ApiResult<TaskResult>> Post([FromBody] TaskModel model)
        {
            try
            {
                var token = Request.Headers["token"];

                if (token == StringValues.Empty)
                    throw new Exception("invalid token");

                var user = await dbContext.Users
                    .FirstOrDefaultAsync(f => f.Token == token.ToString());

                if (user == null)
                    throw new Exception("invalid token");

                var task = model.Id == 0 ? new StorageModels.UserTask() : await dbContext.UserTasks.FirstOrDefaultAsync(f => f.Id == model.Id);

                task.Description = model.Description;
                task.Title = model.Title;
                task.TaskDate = model.Date.Date;
                task.User = user;
                task.UserTaskStatusHistories.Add(new StorageModels.UserTaskStatusHistory { Status = (StorageModels.UserTaskStatusHistory.TaskStatus)model.Status });

                if (task.Id == 0)
                    await dbContext.UserTasks.AddAsync(task);

                await dbContext.SaveChangesAsync();

                return new ApiResult<TaskResult>(new TaskResult(task));
            }
            catch (Exception exception)
            {
                return new ApiResult<TaskResult>(exception);
            }
        }
    }
}
