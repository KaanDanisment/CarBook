using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Results.Concretes
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(true, message, data)
        {
        }
        public SuccessDataResult(T data) : base(true, data)
        {
        }
    }
}
