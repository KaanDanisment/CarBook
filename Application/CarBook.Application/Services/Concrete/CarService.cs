using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Services.Abstract;
using CarBook.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Services.Concrete
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalService _rentalService;
        private readonly ILogger<CarService> _logger;

        public CarService(ICarRepository carRepository, ILogger<CarService> logger, IRentalService rentalService)
        {
            _carRepository = carRepository;
            _logger = logger;
            _rentalService = rentalService;
        }

        public async Task<IResult> TurnFalseCarAvailability(int carId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Car car = await _carRepository.GetByIdAsync(cancellationToken, carId);
                if (car == null)
                {
                    return new ErrorResult("Car not found");
                }
                car.Available = false;
                return new SuccessResult("Car is not available");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while turning false car availability");
                return new ErrorResult(ex.Message);
            }
        }

        public async Task UpdateCarAvailability()
        {
            try
            {
                var rentals = await _rentalService.GetRentalsWithCar(CancellationToken.None);
                if (!rentals.Success)
                {
                    return;
                }
                if (rentals.Data == null || !rentals.Data.Any())
                {
                    _logger.LogInformation("No rentals found");
                    return;
                }
                foreach (var rental in rentals.Data)
                {
                    rental.Car.Available = true;
                    await _carRepository.UpdateAsync(CancellationToken.None, rental.Car);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating car availability");
                throw;
            }
        }

        public async Task<IResult> UpdateCarLocation(int carId, int locationId, CancellationToken cancellationToken)
        {
            try
            {
                Car car = await _carRepository.GetByIdAsync(cancellationToken, carId);
                if(car == null)
                {
                    return new ErrorResult("Car not found");
                }
                if(car.LocationId != locationId)
                {
                    car.LocationId = locationId;
                    await _carRepository.UpdateAsync(cancellationToken, car);
                    return new SuccessResult("Car location updated successfully");
                }
                return new SuccessResult("Car location is already same with the location");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating car location");
                return new ErrorResult(ex.Message);
            }
        }
    }
}
