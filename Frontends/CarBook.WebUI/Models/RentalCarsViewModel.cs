using CarBook.Dto.CarDtos;
using CarBook.Dto.RentalDtos;

namespace CarBook.WebUI.Models
{
    public class RentalCarsViewModel
    {
        public IEnumerable<AvailableRentalCarDto>? AvailableRentalCars { get; set; } 
        public CreateRentalDto? RentalToCreate{ get; set; }
        public string? ErrorMessage { get; set; }
    }
}
