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
    public class CreateContactCommandHandler: IRequestHandler<CreateContactCommand, IResult>
    {
        private readonly IContactRepository _repository;
        public CreateContactCommandHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _repository.CreateAsync(cancellationToken, new Contact
                {
                    Name = request.Name,
                    Email = request.Email,
                    Subject = request.Subject,
                    Message = request.Message,
                    SendDate = request.SendDate
                });
                return new SuccessResult("Contact created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message, "SystemError");
            }
            
        }
    }
}
