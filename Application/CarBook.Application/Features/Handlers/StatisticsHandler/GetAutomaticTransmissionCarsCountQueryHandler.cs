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
    public class GetAutomaticTransmissionCarsCountQueryHandler : IRequestHandler<GetAutomaticTransmissionCarsCountQuery, IDataResult<GetAutomaticTransmissionCarsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetAutomaticTransmissionCarsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetAutomaticTransmissionCarsCountQueryResult>> Handle(GetAutomaticTransmissionCarsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AutomaticTransmissionCarsCountDto dto = await _statisticsRepository.GetAutomaticTransmissionCarsCount(cancellationToken);
                GetAutomaticTransmissionCarsCountQueryResult result = new()
                {
                    CarsCount = dto.AutomaticTransmissionCarsCount
                };
                return new SuccessDataResult<GetAutomaticTransmissionCarsCountQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAutomaticTransmissionCarsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
