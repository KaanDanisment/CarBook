using CarBook.Dto.BrandDtos;
using CarBook.Dto.CarDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarBook.WebUI.Models
{
    public class CreateCarViewModel
    {
        public List<SelectListItem>? Brands { get; set; }
        public CreateCarDto? CarToCreate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
