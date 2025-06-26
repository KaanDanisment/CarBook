using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.BannerCommands;
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
    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, IResult>
    {
        private readonly IBannerRepository _repository;
        public CreateBannerCommandHandler(IBannerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _repository.CreateAsync(cancellationToken, new Banner
                {
                    Title = request.Title,
                    Description = request.Description,
                    VideoDescription = request.VideoDescription,
                    VideoUrl = request.VideoUrl,
                });
                return new SuccessResult("Banner created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
