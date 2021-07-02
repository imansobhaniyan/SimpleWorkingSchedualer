using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public ApiResult<LoginResult> Post([FromBody] LoginModel model)
        {
            return new ApiResult<LoginResult>
            {
                Success = true,
                Data = new LoginResult
                {
                    Success = model.UserName == "admin" && model.Password == "admin",
                    Token = "test token"
                }
            };
        }
    }
}
