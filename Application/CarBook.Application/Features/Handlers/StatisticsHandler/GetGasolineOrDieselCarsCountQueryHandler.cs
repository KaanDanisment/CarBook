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
    public class GetGasolineOrDieselCarsCountQueryHandler : IRequestHandler<GetGasolineOrDieselCarsCountQuery, IDataResult<GetGasolineOrDieselCarsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetGasolineOrDieselCarsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetGasolineOrDieselCarsCountQueryResult>> Handle(GetGasolineOrDieselCarsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                GasolineOrDieselCarsCountDto dto = await _statisticsRepository.GetGasolineOrDieselCarsCount(cancellationToken);
                GetGasolineOrDieselCarsCountQueryResult result = new()
                {
                    GasolineOrDieselCarsCount = dto.GasolineOrDieselCarsCount
                };
                return new SuccessDataResult<GetGasolineOrDieselCarsCountQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetGasolineOrDieselCarsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
