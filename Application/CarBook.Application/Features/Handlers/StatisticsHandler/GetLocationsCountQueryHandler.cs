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
    public class GetLocationsCountQueryHandler : IRequestHandler<GetLocationsCountQuery, IDataResult<GetLocationsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetLocationsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetLocationsCountQueryResult>> Handle(GetLocationsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                LocationCountDto locationCountDto = await _statisticsRepository.GetLocationCount(cancellationToken);
                GetLocationsCountQueryResult getLocationsCountQueryResult = new GetLocationsCountQueryResult
                {
                    LocationsCount = locationCountDto.LocationCount
                };
                return new SuccessDataResult<GetLocationsCountQueryResult>(getLocationsCountQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetLocationsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
