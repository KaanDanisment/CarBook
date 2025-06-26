using CarBook.Dto.AboutDtos;

namespace CarBook.WebUI.Models
{
    public class AboutViewModel
    {
        public IEnumerable<AboutDto>? Abouts { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
