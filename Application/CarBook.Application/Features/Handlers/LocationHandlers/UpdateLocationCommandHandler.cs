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
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, IResult>
    {
        private readonly ILocationRepository _repository;

        public UpdateLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle (UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Location location = await _repository.GetByIdAsync(cancellationToken, request.LocationId);
                if (location == null)
                {
                    return new ErrorResult("Location not found", "BadRequest");
                }
                location.Name = request.Name;
                await _repository.UpdateAsync(cancellationToken, location);
                return new SuccessResult("Location updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
