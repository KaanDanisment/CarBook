using CarBook.Dto.ServiceDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateServiceViewModel
    {
        public ServiceDto? ServiceToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
