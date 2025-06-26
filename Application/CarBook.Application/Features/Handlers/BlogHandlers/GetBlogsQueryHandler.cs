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
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, IDataResult<IEnumerable<GetBlogsQueryResult>>>
    {
        private readonly IBlogRepository _repository;
        public GetBlogsQueryHandler(IBlogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetBlogsQueryResult>>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<Blog> blogs = await _repository.GetAllAsync(cancellationToken);

                IEnumerable<GetBlogsQueryResult> getBlogsQueryResults = blogs.Select(blog => new GetBlogsQueryResult
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    AuthorId = blog.AuthorId,
                    CoverImageUrl = blog.CoverImageUrl,
                    CreatedDate = blog.CreatedDate,
                    CategoryId = blog.CategoryId,
                });

                return new SuccessDataResult<IEnumerable<GetBlogsQueryResult>>(getBlogsQueryResults);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetBlogsQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
