using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Results.CarResults;
using CarBook.Application.RepositoryInterfaces;
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
    public class GetCarToRentByCarIdQueryHandler : IRequestHandler<GetCarToRentByCarIdQuery, IDataResult<GetCarToRentByCarIdQueryResult>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarToRentByCarIdQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IDataResult<GetCarToRentByCarIdQueryResult>> Handle(GetCarToRentByCarIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Car car = await _carRepository.GetAsync(cancellationToken,
                    car => car.CarId == request.CarId,
                    include: car => car.Include(car => car.CarPricings)
                                            .ThenInclude(car => car.Pricing)
                                        .Include(car => car.Brand));

                if(car == null)
                {
                    return new ErrorDataResult<GetCarToRentByCarIdQueryResult>("Car not found", "BadRequest");
                }

                GetCarToRentByCarIdQueryResult result = new GetCarToRentByCarIdQueryResult
                {
                    CarId = car.CarId,
                    Brand = car.Brand.Name,
                    Model = car.Model,
                    Transmission = car.Transmission,
                    CoverImageUrl = car.CoverImageUrl,
                    HourlyPrice = car.CarPricings.Where(cp => cp.PricingId == 2).Select(cp => cp.Amount).FirstOrDefault(),
                    DailyPrice = car.CarPricings.Where(cp => cp.PricingId == 3).Select(cp => cp.Amount).FirstOrDefault(),
                };

                return new SuccessDataResult<GetCarToRentByCarIdQueryResult>(result);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<GetCarToRentByCarIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
