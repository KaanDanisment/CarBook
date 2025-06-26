using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.AboutResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.AboutQueries
{
    public class GetAboutQuery: IRequest<IDataResult<IEnumerable<GetAboutQueryResult>>>
    {
    }
}
