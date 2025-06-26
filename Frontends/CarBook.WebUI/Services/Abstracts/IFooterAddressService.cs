using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.FooterAddressDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IFooterAddressService
    {
        Task<IDataResult<IEnumerable<FooterAddressDto>>> GetAllFooterAddresses();
        Task<IDataResult<FooterAddressDto>> GetFooterAddressById(int id);
        Task<IResult> CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto);
        Task<IResult> UpdateFooterAddress(FooterAddressDto footerAddressDto);
        Task<IResult> DeleteFooterAddress(int id);
    }
}
