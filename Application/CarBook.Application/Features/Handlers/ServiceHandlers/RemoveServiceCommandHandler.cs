using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.ServiceCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.ServiceHandlers
{
    public class RemoveServiceCommandHandler : IRequestHandler<RemoveServiceCommand, IResult>
    {
        private readonly IServiceRepository _repository;

        public RemoveServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Service service = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (service == null)
                {
                    return new ErrorResult("Service not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, service);
                return new SuccessResult("Service removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
