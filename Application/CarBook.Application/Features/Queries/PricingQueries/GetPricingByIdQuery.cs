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
    public class GetPricingByIdQuery: IRequest<IDataResult<GetPricingByIdQueryResult>>
    {
        public int Id { get; set; }

        public GetPricingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
