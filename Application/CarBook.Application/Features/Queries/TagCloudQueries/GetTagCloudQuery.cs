using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.TagCloudResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.TagCloudQueries
{
    public class GetTagCloudQuery: IRequest<IDataResult<IEnumerable<GetTagCloudQueryResult>>>
    {
    }
}
