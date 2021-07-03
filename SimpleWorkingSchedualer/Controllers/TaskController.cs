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
                    .FirstOrDefaultAsync(f => f.Token == token);

                if (user == null)
                    throw new Exception("invalid token");

                return new ApiResult<List<TaskResult>>(user.UserTasks.ConvertAll(f => new TaskResult
                {
                    Id = f.Id,
                    Title = f.Title,
                    Description = f.Description,
                    Date = f.TaskDate,
                    Status = (int)f.UserTaskStatusHistories.OrderByDescending(x => x.CreateDate).Select(x => x.Status).FirstOrDefault()
                }));
            }
            catch (Exception exception)
            {
                return new ApiResult<List<TaskResult>>(exception);
            }
        }
    }
}
