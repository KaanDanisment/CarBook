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
    public class RemoveBannerCommandHandler : IRequestHandler<RemoveBannerCommand, IResult>
    {
        private readonly IBannerRepository _repository;
        public RemoveBannerCommandHandler(IBannerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveBannerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Banner banner = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(banner == null)
                {
                    return new ErrorResult("Banner not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, banner);
                return new SuccessResult("Banner removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
