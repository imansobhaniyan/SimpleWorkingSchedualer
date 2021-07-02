﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.StorageModels
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public UserRole Role { get; set; }

        public enum UserRole
        {
            Admin = 1,
            User = 2
        }
    }
}
