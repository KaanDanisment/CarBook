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
    public class RemoveAboutCommandHandler : IRequestHandler<RemoveAboutCommand, IResult>
    {
        private readonly IAboutRepository _repository;

        public RemoveAboutCommandHandler(IAboutRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                About about = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if(about == null)
                {
                    return new ErrorResult("About not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, about);
                return new SuccessResult("About removed successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message,"SystemError");
            }

        }
    }
}
