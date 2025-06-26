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
    public class GetTheHighestPricedCarForDailyRentalQueryHandler : IRequestHandler<GetTheHighestPricedCarForDailyRentalQuery, IDataResult<GetTheHighestPricedCarForDailyRentalQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetTheHighestPricedCarForDailyRentalQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetTheHighestPricedCarForDailyRentalQueryResult>> Handle(GetTheHighestPricedCarForDailyRentalQuery request, CancellationToken cancellationToken)
        {
            try
            {
                TheHighestPricedCarForDailyRentalDto dto = await _statisticsRepository.GetTheHighestPricedCarForDailyRental(cancellationToken);
                GetTheHighestPricedCarForDailyRentalQueryResult result = new()
                {
                    BrandName = dto.BrandName,
                    CarModel = dto.CarModel,
                    Amount = dto.Amount
                };
                return new SuccessDataResult<GetTheHighestPricedCarForDailyRentalQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetTheHighestPricedCarForDailyRentalQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
