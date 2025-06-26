using CarBook.Dto.BlogDtos;

namespace CarBook.WebUI.Models
{
    public class BlogViewModel
    {
        public BlogDto? Blog { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
