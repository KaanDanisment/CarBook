using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.ContactQueries;
using CarBook.Application.Features.Results.ContactResults;
using CarBook.Application.RepositoryInterfaces;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Features.Handlers.ContactHandlers
{
    public class GetContactByIdQueryHandler: IRequestHandler<GetContactByIdQuery, IDataResult<GetContactByIdQueryResult>>
    {
        private readonly IContactRepository _repository;
        public GetContactByIdQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<GetContactByIdQueryResult>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                Contact contact = await _repository.GetByIdAsync(cancellationToken, request.Id);
                if (contact == null)
                {
                    return new ErrorDataResult<GetContactByIdQueryResult>("Contact not found", "BadRequest");
                }
                GetContactByIdQueryResult getContactByIdQueryResult = new GetContactByIdQueryResult
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Subject = contact.Subject,
                    Message = contact.Message,
                    SendDate = contact.SendDate
                };
                return new SuccessDataResult<GetContactByIdQueryResult>(getContactByIdQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetContactByIdQueryResult>(ex.Message,"SystemError");
            }
            
        }
    }
}
