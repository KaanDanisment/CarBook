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
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, IResult>
    {
        private readonly ICommentRepository _repository;
        public CreateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Comment
                {
                    Name = request.Name,
                    CreatedAt = request.CreatedAt,
                    Description = request.Description,
                    BlogId = request.BlogId
                });
                return new SuccessResult("Comment created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
