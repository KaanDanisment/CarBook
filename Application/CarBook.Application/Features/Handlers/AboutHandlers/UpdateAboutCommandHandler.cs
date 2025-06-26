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
    public class UpdateAboutCommandHandler: IRequestHandler<UpdateAboutCommand, IResult>
    {
        private readonly IAboutRepository _repository;

        public UpdateAboutCommandHandler(IAboutRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                About about = await _repository.GetByIdAsync(cancellationToken, request.AboutId);
                if(about == null)
                {
                    return new ErrorResult("About not found", "BadRequest");
                }
                about.Title = request.Title;
                about.Description = request.Description;
                about.ImageUrl = request.ImageUrl;
                await _repository.UpdateAsync(cancellationToken, about);
                return new SuccessResult("About updated successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
