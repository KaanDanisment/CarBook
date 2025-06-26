using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.LocationDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<LocationService> _logger;

        public LocationService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<LocationService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IResult> CreateLocation(CreateLocationDto createLocationDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Locations", createLocationDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: CreateLocation");
                return new ErrorResult("veriler eklenirken bir sorun oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteLocation(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Locations/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: DeleteLocation");
                return new ErrorResult("veriler silinirken bir sorun oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<LocationDto>>> GetAllLocations()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Locations");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<LocationDto>>(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetAllLocations");
                return new ErrorDataResult<IEnumerable<LocationDto>>("veriler alınırken bir sorun oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<LocationDto>> GetLocationById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Locations/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<LocationDto>(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetLocationById");
                return new ErrorDataResult<LocationDto>("veriler alınırken bir sorun oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateLocation(LocationDto locationDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Locations", locationDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: UpdateLocation");
                return new ErrorResult("veriler güncellenirken bir sorun oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
