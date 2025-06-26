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
    public class GetBlogsWithAuthorQuery: IRequest<IDataResult<IEnumerable<GetBlogsWithAuthorQueryResult>>>
    {
    }
}
