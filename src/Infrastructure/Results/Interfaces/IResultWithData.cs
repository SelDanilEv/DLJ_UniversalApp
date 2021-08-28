using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Results
{
    public interface IResultWithData<T>
    {
        bool IsSuccess { get; }

        string Message { get; set; }

        T GetData { get; }
    }
}
