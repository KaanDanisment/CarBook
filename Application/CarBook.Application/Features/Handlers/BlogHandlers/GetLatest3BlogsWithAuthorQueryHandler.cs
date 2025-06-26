using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.BlogQueries;
using CarBook.Application.Features.Results.BlogResult;
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
    public class GetLatest3BlogsWithAuthorQueryHandler : IRequestHandler<GetLatest3BlogsWithAuthorQuery, IDataResult<IEnumerable<GetLatest3BlogsWithAuthorQueryResult>>>
    {
        private readonly IBlogRepository _repository;

        public GetLatest3BlogsWithAuthorQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetLatest3BlogsWithAuthorQueryResult>>> Handle(GetLatest3BlogsWithAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<Blog> blogs = await _repository.GetLatest3BlogsWithAuthor(cancellationToken);
                
                IEnumerable<GetLatest3BlogsWithAuthorQueryResult> getLatest3BlogsWithAuthorQueryResults = blogs.Select(blog => new GetLatest3BlogsWithAuthorQueryResult
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    AuthorId = blog.AuthorId,
                    AuthorName = blog.Author.Name,
                    CoverImageUrl = blog.CoverImageUrl,
                    CreatedDate = blog.CreatedDate,
                    CategoryId = blog.CategoryId,
                    Description = blog.Description
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetLatest3BlogsWithAuthorQueryResult>>(getLatest3BlogsWithAuthorQueryResults, "Blogs found successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetLatest3BlogsWithAuthorQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
