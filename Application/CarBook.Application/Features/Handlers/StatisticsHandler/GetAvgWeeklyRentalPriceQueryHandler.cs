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
    public class GetAvgWeeklyRentalPriceQueryHandler : IRequestHandler<GetAvgWeeklyRentalPriceQuery, IDataResult<GetAvgWeeklyRentalPriceQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgWeeklyRentalPriceQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetAvgWeeklyRentalPriceQueryResult>> Handle(GetAvgWeeklyRentalPriceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AvgWeeklyRentalPriceDto avgWeeklyRentalPriceDto = await _statisticsRepository.GetAvgWeeklyRentalPrice(cancellationToken);
                GetAvgWeeklyRentalPriceQueryResult result = new()
                {
                    AvgWeeklyRentalPrice = avgWeeklyRentalPriceDto.AvgWeeklyRentalPrice
                };
                return new SuccessDataResult<GetAvgWeeklyRentalPriceQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAvgWeeklyRentalPriceQueryResult>(null, ex.Message);
            }
        }
    }
}
