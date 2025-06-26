using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.TagCloudCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.TagCloudHandlers
{
    public class UpdateTagCloudCommandHandler : IRequestHandler<UpdateTagCloudCommand, IResult>
    {
        private readonly ITagCloudRepository _repository;

        public UpdateTagCloudCommandHandler(ITagCloudRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateTagCloudCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                TagCloud tagCloud = await _repository.GetByIdAsync(cancellationToken, request.TagCloudId);
                if (tagCloud == null)
                {
                    return new ErrorResult("TagCloud not found", "BadRequest");
                }
                tagCloud.Title = request.Title;
                tagCloud.BlogId = request.BlogId;
                await _repository.UpdateAsync(cancellationToken, tagCloud);
                return new SuccessResult("TagCloud updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
