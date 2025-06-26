using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.PricingCommands;
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
    public class UpdatePricingCommandHandler : IRequestHandler<UpdatePricingCommand, IResult>
    {
        private readonly IPricingRepository _repository;

        public UpdatePricingCommandHandler(IPricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdatePricingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Pricing pricing = await _repository.GetByIdAsync(cancellationToken, request.PricingId);
                if (pricing == null)
                {
                    return new ErrorResult("Pricing not found", "BadRequest");
                }
                pricing.Name = request.Name;
                await _repository.UpdateAsync(cancellationToken, pricing);
                return new SuccessResult("Pricing updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
           
        }
    }
}
