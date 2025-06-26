using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Queries.CommentQueries;
using CarBook.Application.Features.Results.CommentResults;
using CarBook.Application.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CommentHandlers
{
    public class GetCommentsCountByBlogIdQueryHandler : IRequestHandler<GetCommentsCountByBlogIdQuery, IDataResult<GetCommentsCountByBlogIdQueryResult>>
    {
        private readonly ICommentRepository _repository;
        private readonly IBlogRepository _blogRepository;

        public GetCommentsCountByBlogIdQueryHandler(ICommentRepository repository, IBlogRepository blogRepository)
        {
            _repository = repository;
            _blogRepository = blogRepository;
        }

        public async Task<IDataResult<GetCommentsCountByBlogIdQueryResult>> Handle(GetCommentsCountByBlogIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var blog = await _blogRepository.GetByIdAsync(cancellationToken, request.BlogId);
                if (blog == null)
                {
                    return new ErrorDataResult<GetCommentsCountByBlogIdQueryResult>("Blog not found", "BadRequest");
                }

                int count = await _repository.GetCommentsCountByBlogIdAsync(request.BlogId, cancellationToken);
                return new SuccessDataResult<GetCommentsCountByBlogIdQueryResult>(new GetCommentsCountByBlogIdQueryResult { CommentsCount = count });
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetCommentsCountByBlogIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
