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
    public class GetElectricCarsCountQueryHandler : IRequestHandler<GetElectricCarsCountQuery, IDataResult<GetElectricCarsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetElectricCarsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetElectricCarsCountQueryResult>> Handle(GetElectricCarsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ElectricCarsCountDto dto = await _statisticsRepository.GetElectricCarsCount(cancellationToken);
                GetElectricCarsCountQueryResult result = new()
                {
                    ElectricCarsCount = dto.ElectricCarsCount
                };
                return new SuccessDataResult<GetElectricCarsCountQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetElectricCarsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
