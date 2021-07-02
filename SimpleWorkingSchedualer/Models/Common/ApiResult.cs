using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Models.Common
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }

        public string Error { get; set; }

        public T Data { get; set; }
    }
}
