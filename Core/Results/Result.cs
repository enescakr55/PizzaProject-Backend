using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class Result : IResult
    {
        public string Message { get; }

        public bool Success { get; }
        public Result(bool success)
        {
            this.Success = success;
        }
        public Result(bool success,string message):this(success)
        {
            this.Message = message;
        }
    }
}
