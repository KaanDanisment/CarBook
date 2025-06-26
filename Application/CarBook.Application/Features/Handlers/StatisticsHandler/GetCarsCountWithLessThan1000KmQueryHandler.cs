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
    public class GetCarsCountWithLessThan1000KmQueryHandler : IRequestHandler<GetCarsCountWithLessThan1000KmQuery, IDataResult<GetCarsCountWithLessThan1000KmQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetCarsCountWithLessThan1000KmQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetCarsCountWithLessThan1000KmQueryResult>> Handle(GetCarsCountWithLessThan1000KmQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CarsCountWithLessThan1000KmDto dto = await _statisticsRepository.GetCarsCountWithLessThan1000Km(cancellationToken);
                GetCarsCountWithLessThan1000KmQueryResult result = new()
                {
                    CarsCountWithLessThan1000Km = dto.CarsCountWithLessThan1000Km
                };
                return new SuccessDataResult<GetCarsCountWithLessThan1000KmQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetCarsCountWithLessThan1000KmQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
