using CarBook.Dto.PricingDtos;

namespace CarBook.WebUI.Models
{
    public class PricingViewModel
    {
        public IEnumerable<PricingDto>? Pricings { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
