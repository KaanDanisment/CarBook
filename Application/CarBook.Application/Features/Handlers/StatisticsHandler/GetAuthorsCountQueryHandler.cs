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
    public class GetAuthorsCountQueryHandler: IRequestHandler<GetAuthorsCountQuery, IDataResult<GetAuthorsCountQueryResult>>
    {
        private readonly IStatisticsRepository _statisticsRepository;
        public GetAuthorsCountQueryHandler(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }
        public async Task<IDataResult<GetAuthorsCountQueryResult>> Handle(GetAuthorsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AuthorCountDto authorCountDto = await _statisticsRepository.GetAuthorCount(cancellationToken);
                GetAuthorsCountQueryResult authorsCountQueryResult = new ()
                {
                    AuthorCount = authorCountDto.AuthorCount
                };
                return new SuccessDataResult<GetAuthorsCountQueryResult>(authorsCountQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetAuthorsCountQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
