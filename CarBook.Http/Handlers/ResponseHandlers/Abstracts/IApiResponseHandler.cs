using CarBook.Application.Common.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Adapters.Handlers.ResponseHandlers.Abstracts
{
    public interface IApiResponseHandler
    {
        Task<IDataResult<T>> HandleApiResponse<T>(HttpResponseMessage response);
        Task<IResult> HandleApiResponse(HttpResponseMessage response);
    }
}
