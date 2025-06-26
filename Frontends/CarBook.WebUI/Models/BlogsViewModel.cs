using CarBook.Dto.BlogDtos;

namespace CarBook.WebUI.Models
{
    public class BlogsViewModel
    {
        public IEnumerable<BlogDto>? Blogs { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
