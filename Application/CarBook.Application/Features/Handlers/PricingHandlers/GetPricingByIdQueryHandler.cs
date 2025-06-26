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
    public class GetPricingByIdQueryHandler : IRequestHandler<GetPricingByIdQuery, IDataResult<GetPricingByIdQueryResult>>
    {
        private readonly IPricingRepository _repository;

        public GetPricingByIdQueryHandler(IPricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetPricingByIdQueryResult>> Handle(GetPricingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Pricing pricing = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (pricing == null)
                {
                    return new ErrorDataResult<GetPricingByIdQueryResult>("Pricing not found", "BadRequest");
                }
                GetPricingByIdQueryResult getPricingByIdQueryResult = new GetPricingByIdQueryResult
                {
                    PricingId = pricing.PricingId,
                    Name = pricing.Name
                };
                return new SuccessDataResult<GetPricingByIdQueryResult>(getPricingByIdQueryResult, "Pricing found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetPricingByIdQueryResult>(ex.Message, "SystemError");
            }
            
        }
    }
}
