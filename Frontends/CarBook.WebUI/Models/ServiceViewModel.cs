using CarBook.Dto.ServiceDtos;

namespace CarBook.WebUI.Models
{
    public class ServiceViewModel
    {
        public IEnumerable<ServiceDto>? Services { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
