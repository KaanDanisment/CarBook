using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.SocialMediaCommands;
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
    public class UpdateSocialMediaHandler : IRequestHandler<UpdateSocialMediaCommand, IResult>
    {
        private readonly ISocialMediaRepository _repository;

        public UpdateSocialMediaHandler(ISocialMediaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                SocialMedia socialMedia = await _repository.GetByIdAsync(cancellationToken, request.SocialMediaId);
                if (socialMedia == null)
                {
                    return new ErrorResult("Social Media Not Found", "BadRequest");
                }
                socialMedia.Name = request.Name;
                socialMedia.Url = request.Url;
                socialMedia.Icon = request.Icon;
                await _repository.UpdateAsync(cancellationToken, socialMedia);
                return new SuccessResult("Social Media Updated Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
