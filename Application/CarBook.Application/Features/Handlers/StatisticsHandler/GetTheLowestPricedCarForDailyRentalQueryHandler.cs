using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Dtos.StatisticsDtos;
using CarBook.Application.Features.Queries.StatisticsQueries;
using CarBook.Application.Features.Results.StatisticsResult;
using CarBook.Application.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.StatisticsHandler
{
    public class GetTheLowestPricedCarForDailyRentalQueryHandler : IRequestHandler<GetTheLowestPricedCarForDailyRentalQuery, IDataResult<GetTheLowestPricedCarForDailyRentalQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetTheLowestPricedCarForDailyRentalQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetTheLowestPricedCarForDailyRentalQueryResult>> Handle(GetTheLowestPricedCarForDailyRentalQuery request, CancellationToken cancellationToken)
        {
            try
            {
                TheLowestPricedCarForDailyRentalDto dto = await _statisticsRepository.GetTheLowestPricedCarForDailyRental(cancellationToken);
                GetTheLowestPricedCarForDailyRentalQueryResult result = new()
                {
                    BrandName = dto.BrandName,
                    CarModel = dto.CarModel,
                    Amount = dto.Amount
                };
                return new SuccessDataResult<GetTheLowestPricedCarForDailyRentalQueryResult>(result);
            }catch(Exception ex)
            {
                return new ErrorDataResult<GetTheLowestPricedCarForDailyRentalQueryResult>(ex.Message);
            }
        }
    }
}
