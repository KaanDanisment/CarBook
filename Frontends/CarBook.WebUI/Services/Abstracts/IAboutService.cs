using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.AboutDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IAboutService
    {
        Task<IDataResult<IEnumerable<AboutDto>>> GetAllAbouts();
        Task<IResult> CreateAbout(CreateAboutDto createAboutDto);
        Task<IDataResult<AboutDto>> GetAboutById(int id);
        Task<IResult> UpdateAbout(AboutDto aboutDto);
        Task<IResult> DeleteAbout(int id);
    }
}
