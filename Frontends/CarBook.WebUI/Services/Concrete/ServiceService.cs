using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.ServiceDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<ServiceService> _logger;

        public ServiceService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<ServiceService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IResult> CreateService(CreateServiceDto createServiceDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Services", createServiceDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: CreateService");
                return new ErrorResult("Servis oluşturulurken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteService(int serviceId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Services/{serviceId}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: DeleteService");
                return new ErrorResult("Servis silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<ServiceDto>>> GetAllService()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Services");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<ServiceDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetAllService");
                return new ErrorDataResult<IEnumerable<ServiceDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<ServiceDto>> GetServiceById(int serviceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Services/{serviceId}");
                var result = await _apiResponseHandler.HandleApiResponse<ServiceDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetServiceById");
                return new ErrorDataResult<ServiceDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IResult> UpdateService(ServiceDto serviceDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Services", serviceDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: UpdateService");
                return new ErrorResult("Servis güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
