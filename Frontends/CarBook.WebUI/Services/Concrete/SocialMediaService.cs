using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.SocialMediaDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<SocialMediaService> _logger;

        public SocialMediaService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<SocialMediaService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/SocialMedias", createSocialMediaDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: CreateSocialMedia");
                return new ErrorResult("Veriler eklenirken bir hata oluştu.", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteSocialMedia(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/SocialMedias/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: DeleteSocialMedia");
                return new ErrorResult("Veriler silinirken bir hata oluştu.", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<SocialMediaDto>>> GetAllSociaMedias()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/SocialMedias");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<SocialMediaDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetAllSociaMedias");
                return new ErrorDataResult<IEnumerable<SocialMediaDto>>("Veriler alınırken bir hata oluştu.", "InternalServerError");
            }
        }

        public async Task<IDataResult<SocialMediaDto>> GetSocialMediaById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/SocialMedias/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<SocialMediaDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetSocialMediaById");
                return new ErrorDataResult<SocialMediaDto>("Veriler alınırken bir hata oluştu.", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateSocialMedia(SocialMediaDto socialMediaDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/SocialMedias", socialMediaDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: UpdateSocialMedia");
                return new ErrorResult("Veriler güncellenirken bir hata oluştu.", "InternalServerError");
            }
        }
    }
}
