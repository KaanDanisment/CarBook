using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Results.CarResults;
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
    public class GetLatest5CarsQueryHandler: IRequestHandler<GetLatest5CarsQuery, IDataResult<IEnumerable<GetLatest5CarsQueryResult>>>
    {
        private readonly ICarRepository _carRepository;
        public GetLatest5CarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IDataResult<IEnumerable<GetLatest5CarsQueryResult>>> Handle(GetLatest5CarsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Car> cars = await _carRepository.GetLatest5Cars(cancellationToken);
               
                IEnumerable<GetLatest5CarsQueryResult> getLatest5CarsQueryResults = cars.Select(car => new GetLatest5CarsQueryResult
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
                    BigImageUrl = car.BigImageUrl,
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetLatest5CarsQueryResult>>(getLatest5CarsQueryResults, "Cars found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetLatest5CarsQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
