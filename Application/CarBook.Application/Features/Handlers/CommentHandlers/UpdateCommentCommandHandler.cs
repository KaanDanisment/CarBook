using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.CommentCommands;
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
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, IResult>
    {
        private readonly ICommentRepository _commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IResult> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Comment comment = await _commentRepository.GetByIdAsync(cancellationToken, request.CommentId);
                if (comment == null)
                {
                    return new ErrorResult("Comment Not Found", "BadRequest");
                }
                comment.Name = request.Name;
                comment.Description = request.Description;

                await _commentRepository.UpdateAsync(cancellationToken, comment);
                return new SuccessResult("Comment updated succesfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
