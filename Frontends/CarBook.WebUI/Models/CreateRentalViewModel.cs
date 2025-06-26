using CarBook.Dto.CarDtos;
using CarBook.Dto.CustomerDtos;
using CarBook.Dto.RentalDtos;

namespace CarBook.WebUI.Models
{
    public class CreateRentalViewModel
    {
        public CarToRentalDto? CarToRental { get; set; }
        public CreateRentalDto? RentalToCreate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
