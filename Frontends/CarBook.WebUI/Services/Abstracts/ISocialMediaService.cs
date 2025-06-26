using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.SocialMediaDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ISocialMediaService
    {
        Task<IDataResult<IEnumerable<SocialMediaDto>>> GetAllSociaMedias();
        Task<IDataResult<SocialMediaDto>> GetSocialMediaById(int id);
        Task<IResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto);
        Task<IResult> UpdateSocialMedia(SocialMediaDto socialMediaDto);
        Task<IResult> DeleteSocialMedia(int id);
    }
}
