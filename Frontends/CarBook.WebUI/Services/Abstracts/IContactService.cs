using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.ContactDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IContactService
    {
        Task<IResult> CreateContact(CreateContactDto createContactDto);
        Task<IDataResult<IEnumerable<ContactDto>>> GetAllContacts();
    }
}
