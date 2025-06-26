using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Application.Features.Queries.CommentQueries;
using CarBook.Application.Features.Results.CommentResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CommentHandlers
{
    public class GetCommentsByBlogIdQueryHandler: IRequestHandler<GetCommentsByBlogIdQuery, IDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogRepository _blogRepository;

        public GetCommentsByBlogIdQueryHandler(ICommentRepository commentRepository, IBlogRepository blogRepository)
        {
            _commentRepository = commentRepository;
            _blogRepository = blogRepository;
        }

        public async Task<IDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>>> Handle(GetCommentsByBlogIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Blog blog = await _blogRepository.GetByIdAsync(cancellationToken,request.Id);
                if (blog == null)
                {
                    return new ErrorDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>>("Blog not found", "BadRequest");
                }
                IEnumerable<Comment> comments = await _commentRepository.GetCommentsByBlogIdAsync(request.Id, cancellationToken);

                IEnumerable<GetCommentsByBlogIdQueryResult> getCommentsByBlogIdQueryResults = comments.Select(comment => new GetCommentsByBlogIdQueryResult
                {
                    CommentId = comment.CommentId,
                    Name = comment.Name,
                    CreatedAt = comment.CreatedAt,
                    Description = comment.Description,
                    BlogId = comment.BlogId,
                }).ToList();

                return new SuccessDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>>(getCommentsByBlogIdQueryResults);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetCommentsByBlogIdQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
