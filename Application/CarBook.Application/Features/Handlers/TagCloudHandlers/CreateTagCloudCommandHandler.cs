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
    public class CreateTagCloudCommandHandler : IRequestHandler<CreateTagCloudCommand, IResult>
    {
        private readonly ITagCloudRepository _repository;

        public CreateTagCloudCommandHandler(ITagCloudRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateTagCloudCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new TagCloud
                {
                    Title = request.Title,
                    BlogId = request.BlogId
                });
                return new SuccessResult("TagCloud created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
