using CarBook.Dto.FooterAddressDtos;

namespace CarBook.WebUI.Models
{
    public class FooterAddressViewModel
    {
        public IEnumerable<FooterAddressDto>? FooterAddresses { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
