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
    public class RemoveAuthorCommandHandler : IRequestHandler<RemoveAuthorCommand, IResult>
    {
        private readonly IAuthorRepository _repository;

        public RemoveAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                Author author = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (author == null)
                {
                    return new ErrorResult("Author Not Found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, author);
                return new SuccessResult("Author removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
