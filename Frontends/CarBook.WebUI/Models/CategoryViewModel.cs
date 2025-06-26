using CarBook.Dto.CategoryDtos;

namespace CarBook.WebUI.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
