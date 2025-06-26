using CarBook.Dto.PricingDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdatePricingViewModel
    {
        public PricingDto? PricingToUpdate { get; set; }
        public string?  ErrorMessage { get; set; }
    }
}
