using CarBook.Dto.AboutDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateAboutViewModel
    {
        public AboutDto? AboutToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
