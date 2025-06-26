using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.BlogQueries;
using CarBook.Application.Features.Results.BlogResult;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.BlogHandlers
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, IDataResult<GetBlogByIdQueryResult>>
    {
        private readonly IBlogRepository _repository;
        public GetBlogByIdQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetBlogByIdQueryResult>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Blog blog = await _repository.GetAsync(cancellationToken, blog => blog.BlogId == request.Id, include: blog => blog.Include(blog => blog.Author).Include(blog => blog.Category));
                if(blog == null)
                {
                    return new ErrorDataResult<GetBlogByIdQueryResult>("Blog not found", "BadRequest");
                }
                GetBlogByIdQueryResult getBlogByIdQueryResult = new GetBlogByIdQueryResult
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    AuthorId = blog.AuthorId,
                    AuthorName = blog.Author.Name,
                    CoverImageUrl = blog.CoverImageUrl,
                    CreatedDate = blog.CreatedDate,
                    CategoryId = blog.CategoryId,
                    Description = blog.Description,
                    AuthorDescription = blog.Author.Description,
                    AuthorImageUrl = blog.Author.ImageUrl
                };
                return new SuccessDataResult<GetBlogByIdQueryResult>(getBlogByIdQueryResult, "Blog found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetBlogByIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
