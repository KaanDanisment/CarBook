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
    public class UpdateCarCommandHandler: IRequestHandler<UpdateCarCommand, IResult>
    {
        private readonly ICarRepository _repository;
        private readonly IBrandRepository _brandRepository;
        public UpdateCarCommandHandler(ICarRepository repository, IBrandRepository brandRepository)
        {
            _repository = repository;
            _brandRepository = brandRepository;
        }

        public async Task<IResult> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Car car = await _repository.GetByIdAsync(cancellationToken, request.CarId);
                if (car == null)
                {
                    return new ErrorResult("Car not found", "BadRequest");
                }
                Brand brand = await _brandRepository.GetByIdAsync(cancellationToken,request.BrandId);
                if(brand == null)
                {
                    return new ErrorResult("Brand not found", "BadRequest");
                }
                car.BrandId = request.BrandId;
                car.Model = request.Model;
                car.CoverImageUrl = request.CoverImageUrl;
                car.Km = request.Km;
                car.Transmission = request.Transmission;
                car.Seat = request.Seat;
                car.Luggage = request.Luggage;
                car.Fuel = request.Fuel;
                car.BigImageUrl = request.BigImageUrl;

                await _repository.UpdateAsync(cancellationToken, car);
                return new SuccessResult("Car updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
