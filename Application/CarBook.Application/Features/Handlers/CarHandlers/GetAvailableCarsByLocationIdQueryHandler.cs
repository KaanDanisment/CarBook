using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Dtos.CarDtos;
using CarBook.Application.Features.Queries.CarQueries;
using CarBook.Application.Features.Results.CarResults;
using CarBook.Application.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CarHandlers
{
    public class GetAvailableCarsByLocationIdQueryHandler : IRequestHandler<GetAvailableCarsByLocationIdQuery, IDataResult<IEnumerable<GetAvailableCarsByLocationIdQueryResult>>>
    {
        private readonly ICarRepository _carRepository;

        public GetAvailableCarsByLocationIdQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IDataResult<IEnumerable<GetAvailableCarsByLocationIdQueryResult>>> Handle(GetAvailableCarsByLocationIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<AvailableCarByLocationDto> cars = await _carRepository.GetAvailableCarsByLocation(cancellationToken, request.LocationId);
                IEnumerable<GetAvailableCarsByLocationIdQueryResult> result = cars.Select(c => new GetAvailableCarsByLocationIdQueryResult
                {
                    CarId = c.CarId,
                    Brand = c.Brand,
                    Model = c.Model,
                    Transmission = c.Transmission,
                    CoverImageUrl = c.CoverImageUrl,
                    BigImageUrl = c.BigImageUrl,
                    HourlyPrice = c.HourlyPrice,
                    DailyPrice = c.DailyPrice,
                    MonthlyPrice = c.MonthlyPrice
                }).ToList();

                return new SuccessDataResult<IEnumerable<GetAvailableCarsByLocationIdQueryResult>>(result);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetAvailableCarsByLocationIdQueryResult>>(ex.Message, "SystemError");    
            }
        }
    }
}
