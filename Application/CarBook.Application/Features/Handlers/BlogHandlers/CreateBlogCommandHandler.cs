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
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, IResult>
    {
        private readonly IBlogRepository _repository;

        public CreateBlogCommandHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _repository.CreateAsync(cancellationToken, new Blog
                {
                    Title = request.Title,
                    AuthorId = request.AuthorId,
                    CoverImageUrl = request.CoverImageUrl,
                    CreatedDate = request.CreatedDate,
                    CategoryId = request.CategoryId
                });
                return new SuccessResult("Blog created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
