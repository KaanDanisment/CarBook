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
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, IResult>
    {
        private readonly ICarRepository _repository;
        private readonly IBrandRepository _brandRepository;
        public CreateCarCommandHandler(ICarRepository repository, IBrandRepository brandRepository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
        }

        public async Task<IResult> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Brand brand = await _brandRepository.GetByIdAsync(cancellationToken,request.BrandId);
                if(brand == null)
                {
                    return new ErrorResult("Brand not found", "BadRequest");
                }
                await _repository.CreateAsync(cancellationToken, new Car
                {
                    BrandId = request.BrandId,
                    Model = request.Model,
                    CoverImageUrl = request.CoverImageUrl,
                    Km = request.Km,
                    Transmission = request.Transmission,
                    Seat = request.Seat,
                    Luggage = request.Luggage,
                    Fuel = request.Fuel,
                    BigImageUrl = request.BigImageUrl
                });
                return new SuccessResult("Car created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }

        }
    }
}
