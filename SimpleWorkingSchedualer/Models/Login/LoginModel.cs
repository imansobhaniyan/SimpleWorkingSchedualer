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
        public bool Success { get; set; }

        public string Token { get; set; }
    }
}
