using CarBook.Dto.BannerDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateBannerViewModel
    {
        public BannerDto? BannerToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
