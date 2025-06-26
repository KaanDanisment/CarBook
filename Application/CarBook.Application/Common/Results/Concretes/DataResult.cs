using CarBook.Application.Common.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Results.Concretes
{
    public class DataResult<T> : IDataResult<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }

        public DataResult(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public DataResult(bool success, T data)
        {
            Success = success;
            Data = data;
        }
    }
}
