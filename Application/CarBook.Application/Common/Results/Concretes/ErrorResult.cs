using CarBook.Application.Common.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Results.Concretes
{
    public class ErrorResult : Result, IResult
    {
        public string ErrorType { get; set; }

        public ErrorResult(string errorType) : base(false)
        {
            ErrorType = errorType;
        }

        public ErrorResult(string message, string errorType) : base(false, message)
        {
            ErrorType = errorType;
        }

    }
}
