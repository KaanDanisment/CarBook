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
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, IResult>
    {
        private readonly IServiceRepository _repository;

        public UpdateServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Service service = await _repository.GetByIdAsync(cancellationToken, request.ServiceId);
                if(service == null)
                {
                    return new ErrorResult("Service not found", "BadRequest");
                }
                service.Title = request.Title;
                service.Description = request.Description;
                service.IconUrl = request.IconUrl;
                await _repository.UpdateAsync(cancellationToken, service);
                return new SuccessResult("Service updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
