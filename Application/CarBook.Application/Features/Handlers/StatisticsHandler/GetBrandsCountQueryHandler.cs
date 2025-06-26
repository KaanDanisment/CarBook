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
    public class GetBrandsCountQueryHandler : IRequestHandler<GetBrandsCountQuery, IDataResult<GetBrandsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetBrandsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetBrandsCountQueryResult>> Handle(GetBrandsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                BrandCountDto brandCountDto = await _statisticsRepository.GetBrandCount(cancellationToken);
                GetBrandsCountQueryResult getBrandsCountQueryResult = new GetBrandsCountQueryResult
                {
                    BrandsCount = brandCountDto.BrandCount
                };
                return new SuccessDataResult<GetBrandsCountQueryResult>(getBrandsCountQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetBrandsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
