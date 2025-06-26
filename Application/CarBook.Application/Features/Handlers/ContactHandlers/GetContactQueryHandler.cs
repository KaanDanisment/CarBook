using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Features.Queries.ContactQueries;
using CarBook.Application.Features.Results.ContactResults;
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
    public class GetContactQueryHandler: IRequestHandler<GetContactQuery, IDataResult<IEnumerable<GetContactQueryResult>>>
    {
        private readonly IContactRepository _repository;
        public GetContactQueryHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task<IDataResult<IEnumerable<GetContactQueryResult>>> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                IEnumerable<Contact> contacts = await _repository.GetAllAsync(cancellationToken);
                
                IEnumerable<GetContactQueryResult> getContactQueryResult = contacts.Select(c => new GetContactQueryResult
                {
                    ContactId = c.ContactId,
                    Name = c.Name,
                    Email = c.Email,
                    Subject = c.Subject,
                    Message = c.Message,
                    SendDate = c.SendDate
                }).ToList();
                return new SuccessDataResult<IEnumerable<GetContactQueryResult>>(getContactQueryResult);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<GetContactQueryResult>>(ex.Message, "SystemError");
            }
           
        }
    }
}
