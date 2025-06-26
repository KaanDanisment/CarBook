using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.CommentQueries;
using CarBook.Application.Features.Results.CommentResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.CommentHandlers
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, IDataResult<IEnumerable<GetCommentQueryResult>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsQueryHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IDataResult<IEnumerable<GetCommentQueryResult>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Comment> comments = await _commentRepository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetCommentQueryResult> getCommentQueryResults = comments.Select(comment => new GetCommentQueryResult
                {
                    CommentId = comment.CommentId,
                    Name = comment.Name,
                    CreatedAt = comment.CreatedAt,
                    Description = comment.Description,
                    BlogId = comment.BlogId,
                }).ToList();

                return new SuccessDataResult<IEnumerable<GetCommentQueryResult>>(getCommentQueryResults);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetCommentQueryResult>>(ex.Message, "SystemError");
            }
        }
    }
}
