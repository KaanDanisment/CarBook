using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.FeatureDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IFeatureService
    {
        Task<IDataResult<IEnumerable<FeatureDto>>> GetAllFeatures();
        Task<IResult> CreateFeature(CreateFeatureDto createFeatureDto);
        Task<IDataResult<FeatureDto>> GetFeatureById(int id);
        Task<IResult> UpdateFeature(FeatureDto featureDto);
        Task<IResult> DeleteFeature(int id);
    }
}
