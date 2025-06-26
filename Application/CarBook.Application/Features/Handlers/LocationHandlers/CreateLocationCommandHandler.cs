using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.LocationCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.LocationHandlers
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, IResult>
    {
        private readonly ILocationRepository _repository;

        public CreateLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Location { Name = request.Name });
                return new SuccessResult("Location created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
