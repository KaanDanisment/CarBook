using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Results.Abstracts
{
    public interface IDataResult<T>
    {
        public bool Success { get; }
        public string Message { get; }
        public T Data { get; }
    }
}
