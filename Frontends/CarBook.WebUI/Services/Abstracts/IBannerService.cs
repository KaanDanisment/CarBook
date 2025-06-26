using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.BannerDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IBannerService
    {
        Task<IDataResult<IEnumerable<BannerDto>>> GetAllBanners();
        Task<IResult> CreateBanner(CreateBannerDto createBannerDto);
        Task<IDataResult<BannerDto>> GetBannerById(int id);
        Task<IResult> UpdateBanner(BannerDto bannerDto);
        Task<IResult> DeleteBanner(int id);
    }
}
