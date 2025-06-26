using CarBook.Dto.CategoryDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateCategoryViewModel
    {
        public CategoryDto? CategoryToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
