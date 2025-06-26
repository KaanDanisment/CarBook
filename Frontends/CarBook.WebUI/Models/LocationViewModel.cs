using CarBook.Dto.LocationDtos;

namespace CarBook.WebUI.Models
{
    public class LocationViewModel
    {
        public IEnumerable<LocationDto>? Locations { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
