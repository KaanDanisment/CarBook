using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.ContactCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.ContactHandlers
{
    public class RemoveContactCommandHandler: IRequestHandler<RemoveContactCommand, IResult>
    {
        private readonly IContactRepository _repository;
        public RemoveContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Contact contact = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (contact == null)
                {
                    return new ErrorResult("Contact not found", "BadRequest");
                }
                await _repository.RemoveAsync(cancellationToken, contact);
                return new SuccessResult("Contact removed successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
