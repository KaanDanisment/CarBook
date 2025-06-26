using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.CarCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CarHandlers
{
    public class RemoveCarCommandHandler:IRequestHandler<RemoveCarCommand, IResult>
    {
        private readonly ICarRepository _repository;
        public RemoveCarCommandHandler(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Car car = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (car == null)
                {
                    return new ErrorResult("Car not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, car);
                return new SuccessResult("Car removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
