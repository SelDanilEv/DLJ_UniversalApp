using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Results
{
    public class Result : Result<string>
    {

    }

    public class Result<T> : IResultWithData<T>, IResult
    {
        public Result()
        {
            Status = ResultStatus.Success;
        }

        public Result(T data) : this()
        {
            Data = data;
        }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public T GetData => Data;

        public bool IsSuccess => Status == ResultStatus.Success;
    }
}
