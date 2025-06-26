using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.BlogResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.BlogQueries
{
    public class GetLatest3BlogsWithAuthorQuery: IRequest<IDataResult<IEnumerable<GetLatest3BlogsWithAuthorQueryResult>>>
    {
    }
}
