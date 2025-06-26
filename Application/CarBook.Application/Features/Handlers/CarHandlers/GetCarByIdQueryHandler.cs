using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Results.CarResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarBook.Application.Features.Handlers.CarHandlers
{
    public class GetCarByIdQueryHandler: IRequestHandler<GetCarByIdQuery, IDataResult<GetCarByIdQueryResult>>
    {
        private readonly ICarRepository _repository;
        public GetCarByIdQueryHandler(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetCarByIdQueryResult>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Car car = await _repository.GetAsync(cancellationToken,
                    car => car.CarId == request.Id,
                    include: car => car.Include(car => car.CarPricings)
                                            .ThenInclude(carPricing => carPricing.Pricing)
                                        .Include(car => car.Brand));
                if(car == null)
                {
                    return new ErrorDataResult<GetCarByIdQueryResult>("Car not found", "BadRequest");
                }
                GetCarByIdQueryResult getCarByIdQueryResult = new GetCarByIdQueryResult
                {
                    CarId = car.CarId,
                    BrandId = car.BrandId,
                    BrandName = car.Brand.Name,
                    CarPricings = car.CarPricings,
                    Model = car.Model,
                    CoverImageUrl = car.CoverImageUrl,
                    Fuel = car.Fuel,
                    Km = car.Km,
                    Transmission = car.Transmission,
                    Seat = car.Seat,
                    Luggage = car.Luggage,
                    BigImageUrl = car.BigImageUrl,
                };
                return new SuccessDataResult<GetCarByIdQueryResult>(getCarByIdQueryResult, "Car found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetCarByIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
