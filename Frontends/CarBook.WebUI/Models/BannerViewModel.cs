using CarBook.Dto.BannerDtos;

namespace CarBook.WebUI.Models
{
    public class BannerViewModel
    {
        public IEnumerable<BannerDto>? Banners { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
