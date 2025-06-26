using CarBook.Dto.AuthorDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateAuthorViewModel
    {
        public AuthorDto? AuthorToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
