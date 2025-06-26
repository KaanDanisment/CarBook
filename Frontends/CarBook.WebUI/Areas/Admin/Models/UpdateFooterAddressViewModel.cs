using CarBook.Dto.FooterAddressDtos;

namespace CarBook.WebUI.Areas.Admin.Models
{
    public class UpdateFooterAddressViewModel
    {
        public FooterAddressDto? FooterAddressToUpdate { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
