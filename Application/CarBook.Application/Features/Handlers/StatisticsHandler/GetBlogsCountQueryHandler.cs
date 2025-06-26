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
    public class GetBlogsCountQueryHandler : IRequestHandler<GetBlogsCountQuery, IDataResult<GetBlogsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetBlogsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetBlogsCountQueryResult>> Handle(GetBlogsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                BlogCountDto blogCountDto = await _statisticsRepository.GetBlogCount(cancellationToken);
                GetBlogsCountQueryResult getBlogsCountQueryResult = new GetBlogsCountQueryResult
                {
                    BlogsCount = blogCountDto.BlogCount
                };
                return new SuccessDataResult<GetBlogsCountQueryResult>(getBlogsCountQueryResult);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<GetBlogsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
