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
    public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand, IResult>
    {
        private readonly ICommentRepository _commentRepository;

        public RemoveCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IResult> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Comment comment = await _commentRepository.GetByIdAsync(cancellationToken,request.Id);
                if(comment == null)
                {
                    return new ErrorResult("Comment not found", "BadRequest");
                }
                await _commentRepository.RemoveAsync(cancellationToken, comment);
                return new SuccessResult("Comment deleted succesfully");
            }catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
