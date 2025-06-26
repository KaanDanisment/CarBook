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
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand, IResult>
    {
        private readonly IBannerRepository _repository;
        public UpdateBannerCommandHandler(IBannerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Banner banner = await _repository.GetByIdAsync(cancellationToken, request.BannerId);
                if(banner == null)
                {
                    return new ErrorResult("Banner not found", "BadRequest");
                }
                banner.Title = request.Title;
                banner.Description = request.Description;
                banner.VideoDescription = request.VideoDescription;
                banner.VideoUrl = request.VideoUrl;
                await _repository.UpdateAsync(cancellationToken, banner);
                return new SuccessResult("Banner updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
