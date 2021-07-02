using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWorkingSchedualer.Models.Common
{
    public class ApiResult<T>
    {
        public ApiResult(Exception exception)
        {
            Error = exception.Message;
        }

        public ApiResult(T data)
        {
            Success = true;
            Data = data;
        }

        public bool Success { get; set; }

        public string Error { get; set; }

        public T Data { get; set; }
    }
}
