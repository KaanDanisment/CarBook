using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.SocialMediaQueries;
using CarBook.Application.Features.Results.SocialMediaResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.SocialMediaHandlers
{
    public class GetSocialMediaQueryHandler : IRequestHandler<GetSocialMediaQuery, IDataResult<IEnumerable<GetSocialMediaQueryResult>>>
    {
        private readonly ISocialMediaRepository _repository;

        public GetSocialMediaQueryHandler(ISocialMediaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetSocialMediaQueryResult>>> Handle(GetSocialMediaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<SocialMedia> socialMedias = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetSocialMediaQueryResult> getSocialMediaQueryResults = socialMedias.Select(socialMedia => new GetSocialMediaQueryResult
                {
                    SocialMediaId = socialMedia.SocialMediaId,
                    Name = socialMedia.Name,
                    Url = socialMedia.Url,
                    Icon = socialMedia.Icon
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetSocialMediaQueryResult>>(getSocialMediaQueryResults, "Social Media Found Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetSocialMediaQueryResult>>(ex.Message, "SystemError");
            }
            
        }
    }
}
