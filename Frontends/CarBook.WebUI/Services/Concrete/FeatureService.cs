using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.FeatureDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<FeatureService> _logger;

        public FeatureService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<FeatureService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IDataResult<IEnumerable<FeatureDto>>> GetAllFeatures()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Features");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<FeatureDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all features.");
                return new ErrorDataResult<IEnumerable<FeatureDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Features", createFeatureDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a feature.");
                return new ErrorResult("Özellik oluşturulurken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IDataResult<FeatureDto>> GetFeatureById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Features/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<FeatureDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting a feature by id.");
                return new ErrorDataResult<FeatureDto>("Veri yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IResult> UpdateFeature(FeatureDto featureDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Features", featureDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a feature.");
                return new ErrorResult("Özellik güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteFeature(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Features/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a feature.");
                return new ErrorResult("Özellik silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
