﻿using CarBook.Application.Common.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Results.Concretes
{
    public class Result : IResult
    {
        public bool Success { get; }
        public string Message { get; }

        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }
    }
}
