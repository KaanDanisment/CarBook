using CarBook.Dto.CarDtos;

namespace CarBook.WebUI.Models
{
    public class CarViewModel
    {
        public IEnumerable<CarDto>? Cars { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
