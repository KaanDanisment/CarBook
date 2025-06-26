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
    public class GetTheBlogWithTheMostCommentsQueryHandler : IRequestHandler<GetTheBlogWithTheMostCommentsQuery, IDataResult<GetTheBlogWithTheMostCommentsQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetTheBlogWithTheMostCommentsQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IDataResult<GetTheBlogWithTheMostCommentsQueryResult>> Handle(GetTheBlogWithTheMostCommentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                TheBlogWithTheMostCommentsDto dto = await _statisticsRepository.GetTheBlogWithTheMostComments(cancellationToken);
                GetTheBlogWithTheMostCommentsQueryResult result = new()
                {
                    BlogTitle = dto.BlogTitle
                };
                return new SuccessDataResult<GetTheBlogWithTheMostCommentsQueryResult>(result);
            }
            catch(Exception ex)
            {
                return new ErrorDataResult<GetTheBlogWithTheMostCommentsQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
