using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Results.CarResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CarHandlers
{
    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, IDataResult<IEnumerable<GetCarsQueryResult>>>
    {
        private readonly ICarRepository _carRepository;
        public GetCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IDataResult<IEnumerable<GetCarsQueryResult>>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Car> cars = await _carRepository.GetAllAsync(cancellationToken, include: car => car.Include(car => car.CarPricings).ThenInclude(carPricing => carPricing.Pricing).Include(car => car.Brand));
               
                IEnumerable<GetCarsQueryResult> getCarsQueryResults = cars.Select(car => new GetCarsQueryResult
                {
                    CarId = car.CarId,
                    BrandId = car.BrandId,
                    BrandName = car.Brand.Name,
                    CarPricings = car.CarPricings,
                    Model = car.Model,
                    CoverImageUrl = car.CoverImageUrl,
                    Km = car.Km,
                    Transmission = car.Transmission,
                    Seat = car.Seat,
                    Luggage = car.Luggage,
                    Fuel = car.Fuel,
                    BigImageUrl = car.BigImageUrl
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetCarsQueryResult>>(getCarsQueryResults, "Cars found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetCarsQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
