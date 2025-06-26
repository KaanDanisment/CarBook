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
    public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, IResult>
    {
        private readonly ISocialMediaRepository _repository;

        public CreateSocialMediaCommandHandler(ISocialMediaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new SocialMedia
                {
                    Name = request.Name,
                    Url = request.Url,
                    Icon = request.Icon
                });
                return new SuccessResult("Social Media Created Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
           
        }
    }
}
