using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.BlogCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.BlogHandlers
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand,IResult>
    {
        private readonly IBlogRepository _repository;
        public UpdateBlogCommandHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Blog blog = await _repository.GetByIdAsync(cancellationToken, request.BlogId);
                if (blog == null)
                {
                    return new ErrorResult("Blog not found", "BadRequest");
                }
                await _repository.UpdateAsync(cancellationToken, new Blog
                {
                    Title = request.Title,
                    AuthorId = request.AuthorId,
                    CoverImageUrl = request.CoverImageUrl,
                    CategoryId = request.CategoryId
                });
                return new SuccessResult("Blog updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
