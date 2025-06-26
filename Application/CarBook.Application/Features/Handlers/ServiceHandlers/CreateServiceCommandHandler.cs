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
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, IResult>
    {
        private readonly IServiceRepository _repository;

        public CreateServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Service
                {
                    Title = request.Title,
                    Description = request.Description,
                    IconUrl = request.IconUrl
                });
                return new SuccessResult("Service created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
