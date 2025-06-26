using CarBook.Dto.CarDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarBook.WebUI.Models
{
    public class UpdateCarViewModel
    {
        public List<SelectListItem>? Brands { get; set; }
        public CarDto? CarToUpdate { get; set; }
        public string? ErrorMessage{ get; set; }
    }
}
