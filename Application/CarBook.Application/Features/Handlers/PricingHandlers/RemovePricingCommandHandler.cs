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
    public class RemovePricingCommandHandler : IRequestHandler<RemovePricingCommand, IResult>
    {
        private readonly IPricingRepository _repository;

        public RemovePricingCommandHandler(IPricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemovePricingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Pricing pricing = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(pricing == null)
                {
                    return new ErrorResult("Pricing not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, pricing);
                return new SuccessResult("Pricing removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
           
        }
    }
}
