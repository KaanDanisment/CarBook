using CarBook.Dto.SocialMediaDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateSocialMediaViewModel
    {
        public SocialMediaDto? SocialMediaToUpdate { get; set; }
        public string ErrorMessage { get; set; }
    }
}
