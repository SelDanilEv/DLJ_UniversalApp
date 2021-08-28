using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Results
{
    public interface IResult
    {
        bool IsSuccess { get; }

        string Message { get; set; }
    }
}
