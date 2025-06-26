using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.AboutCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.AboutHandlers
{
    public class CreateAboutCommandHandler: IRequestHandler<CreateAboutCommand, IResult>
    {
        private readonly IAboutRepository _repository;
        public CreateAboutCommandHandler(IAboutRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                About about = new About
                {
                    Title = request.Title,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl
                };
                await _repository.CreateAsync(cancellationToken, about);
                return new SuccessResult("About created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message,"SystemError");
            }
        }
    }
}
