using CarBook.Dto.ContactDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class ContactViewModel
    {
        public IEnumerable<ContactDto>? Contacts { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
