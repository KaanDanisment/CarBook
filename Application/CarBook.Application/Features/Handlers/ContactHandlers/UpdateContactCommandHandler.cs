using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Commands.ContactCommands;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.ContactHandlers
{
    public class UpdateContactCommandHandler: IRequestHandler<UpdateContactCommand, IResult>
    {
        private readonly IContactRepository _repository;
        public UpdateContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var contact = await _repository.GetByIdAsync(cancellationToken, request.ContactId);
                if(contact == null)
                {
                    return new ErrorResult("Contact not found", "BadRequest");
                }
                contact.Name = request.Name;
                contact.Email = request.Email;
                contact.Subject = request.Subject;
                contact.Message = request.Message;
                contact.SendDate = request.SendDate;
                await _repository.UpdateAsync(cancellationToken, contact);
                return new SuccessResult("Contact updated successfully");
            }
            catch(Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
