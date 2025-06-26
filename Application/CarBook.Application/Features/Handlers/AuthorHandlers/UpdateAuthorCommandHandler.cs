using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.AuthorCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.AuthorHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, IResult>
    {
        private readonly IAuthorRepository _repository;
        public UpdateAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Author author = await _repository.GetByIdAsync(cancellationToken, request.AuthorId);
                if (author == null)
                {
                    return new ErrorResult("Author Not Found", "BadRequest");
                }
                author.Name = request.Name;
                author.ImageUrl = request.ImageUrl;
                author.Description = request.Description;
                await _repository.UpdateAsync(cancellationToken, author);
                return new SuccessResult("Author updated successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
