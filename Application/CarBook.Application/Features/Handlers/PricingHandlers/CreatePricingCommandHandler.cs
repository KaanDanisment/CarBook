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
    public class CreatePricingCommandHandler : IRequestHandler<CreatePricingCommand, IResult>
    {
        private readonly IPricingRepository _repository;

        public CreatePricingCommandHandler(IPricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreatePricingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Pricing
                {
                    Name = request.Name
                });
                return new SuccessResult("Pricing created successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
