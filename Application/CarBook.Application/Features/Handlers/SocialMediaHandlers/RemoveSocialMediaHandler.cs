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
    public class RemoveSocialMediaHandler : IRequestHandler<RemoveSocialMediaCommand, IResult>
    {
        private readonly ISocialMediaRepository _repository;

        public RemoveSocialMediaHandler(ISocialMediaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveSocialMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                SocialMedia socialMedia = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (socialMedia == null)
                {
                    return new ErrorResult("Social Media Not Found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, socialMedia);
                return new SuccessResult("Social Media Removed Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
