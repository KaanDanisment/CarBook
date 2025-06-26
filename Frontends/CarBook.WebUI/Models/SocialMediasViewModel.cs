using CarBook.Dto.SocialMediaDtos;

namespace CarBook.WebUI.Models
{
    public class SocialMediasViewModel
    {
        public IEnumerable<SocialMediaDto>? SocialMedias { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
