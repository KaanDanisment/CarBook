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
    public class GetBannerByIdQueryHandler : IRequestHandler<GetBannerByIdQuery, IDataResult<GetBannerByIdQueryResult>>
    {
        private readonly IBannerRepository _repository;
        public GetBannerByIdQueryHandler(IBannerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetBannerByIdQueryResult>> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Banner banner = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (banner == null)
                {
                    return new ErrorDataResult<GetBannerByIdQueryResult>("Banner not found", "BadRequest");
                }
                GetBannerByIdQueryResult getBannerByIdQueryResult = new GetBannerByIdQueryResult
                {
                    BannerId = banner.BannerId,
                    Title = banner.Title,
                    Description = banner.Description,
                    VideoDescription = banner.VideoDescription,
                    VideoUrl = banner.VideoUrl
                };

                return new SuccessDataResult<GetBannerByIdQueryResult>(getBannerByIdQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetBannerByIdQueryResult>(ex.Message, "SystemError");
            }

        }
    }
}
