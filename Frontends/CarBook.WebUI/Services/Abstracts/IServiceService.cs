using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.ServiceDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IServiceService
    {
        Task<IDataResult<IEnumerable<ServiceDto>>> GetAllService();
        Task<IDataResult<ServiceDto>> GetServiceById(int serviceId);
        Task<IResult> CreateService(CreateServiceDto createServiceDto);
        Task<IResult> UpdateService(ServiceDto serviceDto);
        Task<IResult> DeleteService(int serviceId);
    }
}
