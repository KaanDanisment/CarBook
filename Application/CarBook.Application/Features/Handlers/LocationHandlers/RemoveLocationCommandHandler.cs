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
    public class RemoveLocationCommandHandler : IRequestHandler<RemoveLocationCommand, IResult>
    {
        private readonly ILocationRepository _repository;

        public RemoveLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Location location = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(location == null)
                {
                    return new ErrorResult("Location not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, location);
                return new SuccessResult("Location removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
