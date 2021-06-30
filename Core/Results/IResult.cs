using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
    }
}
