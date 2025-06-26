using CarBook.Dto.BrandDtos;

namespace CarBook.WebUI.Models
{
    public class UpdateBrandViewModel
    {
        public BrandDto? BrandToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
