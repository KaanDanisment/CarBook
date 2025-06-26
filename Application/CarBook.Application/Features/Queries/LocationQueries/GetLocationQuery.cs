using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.LocationResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.LocationQueries
{
    public class GetLocationQuery: IRequest<IDataResult<IEnumerable<GetLocationQueryResult>>>
    {
    }
}
