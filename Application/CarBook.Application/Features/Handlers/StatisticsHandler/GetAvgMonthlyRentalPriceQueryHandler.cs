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
    public class GetAvgMonthlyRentalPriceQueryHandler : IRequestHandler<GetAvgMonthlyRentalPriceQuery, IDataResult<GetAvgMonthlyRentalPriceQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAvgMonthlyRentalPriceQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetAvgMonthlyRentalPriceQueryResult>> Handle(GetAvgMonthlyRentalPriceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AvgMonthlyRentalPriceDto dto = await _statisticsRepository.GetAvgMonthlyRentalPrice(cancellationToken);
                GetAvgMonthlyRentalPriceQueryResult result = new()
                {
                    AvgMonthlyRentalPrice = dto.AvgMonthlyRentalPrice
                };
                return new SuccessDataResult<GetAvgMonthlyRentalPriceQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAvgMonthlyRentalPriceQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
