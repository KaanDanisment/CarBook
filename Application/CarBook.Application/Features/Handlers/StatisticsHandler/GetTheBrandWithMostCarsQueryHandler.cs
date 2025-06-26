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
    public class GetTheBrandWithMostCarsQueryHandler : IRequestHandler<GetTheBrandWithMostCarsQuery, IDataResult<GetTheBrandWithMostCarsQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetTheBrandWithMostCarsQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetTheBrandWithMostCarsQueryResult>> Handle(GetTheBrandWithMostCarsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                TheBrandWithMostCarsDto dto = await _statisticsRepository.GetTheBrandWithMostCars(cancellationToken);
                GetTheBrandWithMostCarsQueryResult result = new()
                {
                    BrandName = dto.BrandName,
                    CarsCount = dto.CarsCount
                };
                return new SuccessDataResult<GetTheBrandWithMostCarsQueryResult>(result);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetTheBrandWithMostCarsQueryResult>(ex.Message);
            }
        }
    }
}
