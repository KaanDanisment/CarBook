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
    public class GetCarsCountQueryHandler : IRequestHandler<GetCarsCountQuery, IDataResult<GetCarsCountQueryResult>>
    {
        private readonly IStatisticsRepository _repository;

        public GetCarsCountQueryHandler(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetCarsCountQueryResult>> Handle(GetCarsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CarCountDto carsCountDto  = await _repository.GetCarsCount(cancellationToken);
                GetCarsCountQueryResult getCarsCountQueryResult = new GetCarsCountQueryResult
                {
                    CarCount = carsCountDto.CarCount
                };
                return new SuccessDataResult<GetCarsCountQueryResult>(getCarsCountQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetCarsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
