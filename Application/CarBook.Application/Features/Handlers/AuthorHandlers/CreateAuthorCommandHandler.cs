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
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, IResult>
    {
        private readonly IAuthorRepository _repository;

        public CreateAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                await _repository.CreateAsync(cancellationToken, new Author
                {
                    Name = request.Name,
                    ImageUrl = request.ImageUrl,
                    Description = request.Description
                });
                return new SuccessResult("Author created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
        }
    }
}
