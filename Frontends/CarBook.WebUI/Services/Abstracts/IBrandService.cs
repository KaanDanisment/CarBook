using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.BrandDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IBrandService
    {
        Task<IDataResult<IEnumerable<BrandDto>>> GetAllBrands();
        Task<IResult> CreateBrand(CreateBrandDto createBrandDto);
        Task<IDataResult<BrandDto>> GetBrandById(int id);
        Task<IResult> UpdateBrand(BrandDto brandDto);
        Task<IResult> DeleteBrand(int id);
    }
}
