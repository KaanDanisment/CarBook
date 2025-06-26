using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.PricingDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IPricingService
    {
        Task<IDataResult<IEnumerable<PricingDto>>> GetAllPricing();
        Task<IResult> CreatePricing(CreatePricingDto createPricingDto);
        Task<IResult> UpdadetePricing(PricingDto pricingDto);
        Task<IDataResult<PricingDto>> GetPricingById(int id);
        Task<IResult> DeletePricing(int id);
    }
}
