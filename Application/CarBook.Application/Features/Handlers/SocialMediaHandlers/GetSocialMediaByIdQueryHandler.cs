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
    public class GetSocialMediaByIdQueryHandler : IRequestHandler<GetSocialMediaByIdQuery, IDataResult<GetSocialMediaByIdQueryResult>>
    {
        private readonly ISocialMediaRepository _repository;

        public GetSocialMediaByIdQueryHandler(ISocialMediaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetSocialMediaByIdQueryResult>> Handle(GetSocialMediaByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                SocialMedia socialMedia = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (socialMedia == null)
                {
                    return new ErrorDataResult<GetSocialMediaByIdQueryResult>("Social Media Not Found", "BadRequest");
                }
                GetSocialMediaByIdQueryResult getSocialMediaByIdQueryResult = new GetSocialMediaByIdQueryResult
                {
                    SocialMediaId = socialMedia.SocialMediaId,
                    Name = socialMedia.Name,
                    Url = socialMedia.Url,
                    Icon = socialMedia.Icon
                };
                return new SuccessDataResult<GetSocialMediaByIdQueryResult>(getSocialMediaByIdQueryResult, "Social Media Found Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetSocialMediaByIdQueryResult>(ex.Message, "SystemError");
            }

        }
    }
}
