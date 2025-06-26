using CarBook.Dto.LocationDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateLocationViewModel
    {
        public LocationDto? LocationToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
