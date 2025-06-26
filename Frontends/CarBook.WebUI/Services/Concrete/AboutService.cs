using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.AboutDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Interfaces
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly ILogger<AboutService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public AboutService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AboutService> logger, IApiResponseHandler apiResponseHandler)
        {
            _client = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<IEnumerable<AboutDto>>> GetAllAbouts()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Abouts");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<AboutDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllAbouts");
                return new ErrorDataResult<IEnumerable<AboutDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Abouts", createAboutDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateAbout");
                return new ErrorResult("Veri eklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<AboutDto>> GetAboutById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Abouts/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<AboutDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAboutById");
                return new ErrorDataResult<AboutDto>("Veri yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateAbout(AboutDto aboutDto)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Abouts", aboutDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateAbout");
                return new ErrorResult("Veri güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteAbout(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"{_baseUrl}/api/Abouts/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteAbout");
                return new ErrorResult("Veri silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
