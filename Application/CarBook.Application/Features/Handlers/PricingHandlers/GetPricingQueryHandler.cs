using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.PricingQueries;
using CarBook.Application.Features.Results.PricingResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.PricingHandlers
{
    public class GetPricingQueryHandler : IRequestHandler<GetPricingQuery, IDataResult<IEnumerable<GetPricingQueryResult>>>
    {
        private readonly IPricingRepository _repository;

        public GetPricingQueryHandler(IPricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetPricingQueryResult>>> Handle(GetPricingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Pricing> pricings = await _repository.GetAllAsync(cancellationToken);
               
                IEnumerable<GetPricingQueryResult> getPricingQueryResults =  pricings.Select(pricing => new GetPricingQueryResult
                {
                    PricingId = pricing.PricingId,
                    Name = pricing.Name
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetPricingQueryResult>>(getPricingQueryResults, "Pricing found successfully");
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetPricingQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
