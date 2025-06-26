using CarBook.Dto.BrandDtos;

namespace CarBook.WebUI.Models
{
    public class BrandViewModel
    {
        public IEnumerable<BrandDto>? Brands { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
