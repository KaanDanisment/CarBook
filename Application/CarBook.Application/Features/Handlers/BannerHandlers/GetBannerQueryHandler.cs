using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.BannerQueries;
using CarBook.Application.Features.Results.BannerResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.BannerHandlers
{
    public class GetBannerQueryHandler : IRequestHandler<GetBannerQuery, IDataResult<IEnumerable<GetBannerQueryResult>>>
    {
        private readonly IBannerRepository _repository;
        public GetBannerQueryHandler(IBannerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetBannerQueryResult>>> Handle(GetBannerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<Banner> values = await _repository.GetAllAsync(cancellationToken);
               
                IEnumerable<GetBannerQueryResult> getBannerQueryResults = values.Select(x => new GetBannerQueryResult
                {
                    BannerId = x.BannerId,
                    Title = x.Title,
                    Description = x.Description,
                    VideoDescription = x.VideoDescription,
                    VideoUrl = x.VideoUrl
                }).ToList();

                return new SuccessDataResult<IEnumerable<GetBannerQueryResult>>(getBannerQueryResults);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetBannerQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
