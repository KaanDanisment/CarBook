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
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, IDataResult<GetCommentByIdQueryResult>>
    {
        private readonly ICommentRepository _repository;

        public GetCommentByIdQueryHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetCommentByIdQueryResult>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Comment comment = await _repository.GetByIdAsync(cancellationToken,request.Id);
                if(comment == null)
                {
                    return new ErrorDataResult<GetCommentByIdQueryResult>("Comment not found", "BadRequest");
                }
                GetCommentByIdQueryResult getCommentByIdQueryResult = new GetCommentByIdQueryResult
                {
                    CommentId = comment.CommentId,
                    Name = comment.Name,
                    CreatedAt = comment.CreatedAt,
                    Description = comment.Description,
                    BlogId = comment.BlogId
                };
                return new SuccessDataResult<GetCommentByIdQueryResult>(getCommentByIdQueryResult, "Comment retrieved successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetCommentByIdQueryResult>(ex.Message, "SystemError");
            }
        }
    }
}
