using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Results.Concretes
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public string ErrorType { get; set; }

        public ErrorDataResult(string errorType) : base(false, default)
        {
            ErrorType = errorType;
        }

        public ErrorDataResult(string message, string errorType) : base(false, message, default)
        {
            ErrorType = errorType;
        }

        public ErrorDataResult(bool success, string message, T data, string errorType) : base(false, message, data)
        {
            ErrorType = errorType;
        }
    }
}
