using CarBook.Dto.AuthorDtos;

namespace CarBook.WebUI.Models
{
    public class AuthorViewModel
    {
        public IEnumerable<AuthorDto>? Authors { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
