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
    public class GetAvgDailyRentalPriceQueryHandler : IRequestHandler<GetAvgDailyRentalPriceQuery, IDataResult<GetAvgDailyRentalPriceQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgDailyRentalPriceQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetAvgDailyRentalPriceQueryResult>> Handle(GetAvgDailyRentalPriceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AvgDailyRentalPriceDto avgDailyRentalPriceDto = await _statisticsRepository.GetAvgDailyRentalPrice(cancellationToken);
                GetAvgDailyRentalPriceQueryResult getAvgDailyRentalPriceQueryResult = new()
                {
                    AvgDailyRentalPrice = avgDailyRentalPriceDto.AvgDailyRentalPrice
                };
                return new SuccessDataResult<GetAvgDailyRentalPriceQueryResult>(getAvgDailyRentalPriceQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAvgDailyRentalPriceQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
