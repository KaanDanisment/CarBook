using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.LocationDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface ILocationService
    {
        Task<IDataResult<IEnumerable<LocationDto>>> GetAllLocations();
        Task<IDataResult<LocationDto>> GetLocationById(int id);
        Task<IResult> CreateLocation(CreateLocationDto createLocationDto);
        Task<IResult> UpdateLocation(LocationDto locationDto);
        Task<IResult> DeleteLocation(int id);
    }
}
