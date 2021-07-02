using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Models.Login
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class LoginResult
    {
        private LoginResult(string token)
        {
            Success = !string.IsNullOrWhiteSpace(token);
            Token = token;
        }

        public bool Success { get; private set; }

        public string Token { get; private set; }

        public static LoginResult FailedResult()
        {
            return new LoginResult(null);
        }

        public static LoginResult SuccessResult(string token)
        {
            return new LoginResult(token);
        }
    }
}
