using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SimpleWorkingSchedualer.DataAccessLayer;
using SimpleWorkingSchedualer.Models.Common;
using SimpleWorkingSchedualer.Models.Login;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SimpleWorkingSchedualerDbContext dbContext;

        public LoginController(SimpleWorkingSchedualerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ApiResult<LoginResult>> Post([FromBody] LoginModel model)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(f => f.UserName == model.UserName && f.Password == model.Password);

                if (user == null)
                    return new ApiResult<LoginResult>(LoginResult.FailedResult());

                string token;

                var random = new Random(DateTime.Now.Millisecond);

                do
                {
                    token = string.Concat(Enumerable.Range(0, 100).Select(f => (char)random.Next('A', 'z')));
                } while (await dbContext.Users.AnyAsync(f => f.Token == token));

                user.Token = token;

                await dbContext.SaveChangesAsync();

                return new ApiResult<LoginResult>(LoginResult.SuccessResult(token));
            }
            catch (Exception exception)
            {
                return new ApiResult<LoginResult>(exception);
            }
        }
    }
}
