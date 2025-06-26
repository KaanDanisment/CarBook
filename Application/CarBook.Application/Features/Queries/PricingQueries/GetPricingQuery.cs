using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Results.PricingResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Queries.PricingQueries
{
    public class GetPricingQuery: IRequest<IDataResult<IEnumerable<GetPricingQueryResult>>>
    {
    }
}
