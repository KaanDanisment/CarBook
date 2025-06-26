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
    public class GetBlogsWithAuthorQueryHandler : IRequestHandler<GetBlogsWithAuthorQuery, IDataResult<IEnumerable<GetBlogsWithAuthorQueryResult>>>
    {
        private readonly IBlogRepository _repository;

        public GetBlogsWithAuthorQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetBlogsWithAuthorQueryResult>>> Handle(GetBlogsWithAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<Blog> blogs = await _repository.GetAllAsync(cancellationToken, include: blog => blog.Include(blog => blog.Author).Include(blog => blog.Category));
                
                IEnumerable<GetBlogsWithAuthorQueryResult> getBlogsWithAuthorQueryResults = blogs.Select(blog => new GetBlogsWithAuthorQueryResult
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    AuthorId = blog.AuthorId,
                    AuthorName = blog.Author.Name,
                    CoverImageUrl = blog.CoverImageUrl,
                    CreatedDate = blog.CreatedDate,
                    CategoryId = blog.CategoryId,
                    CategoryName = blog.Category.Name,
                    Description = blog.Description
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetBlogsWithAuthorQueryResult>>(getBlogsWithAuthorQueryResults, "Blogs found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetBlogsWithAuthorQueryResult>>(ex.Message, "SystemError");
            }

        }
    }
}
