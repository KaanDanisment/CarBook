using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.BannerDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class BannerService : IBannerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<BannerService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public BannerService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<BannerService> logger, IApiResponseHandler apiResponseHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<IEnumerable<BannerDto>>> GetAllBanners()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Banners");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<BannerDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllBanners");
                return new ErrorDataResult<IEnumerable<BannerDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Banners", createBannerDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateBanner");
                return new ErrorResult("Veri oluşturulurken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<BannerDto>> GetBannerById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Banners/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<BannerDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetBannerById");
                return new ErrorDataResult<BannerDto>("Veri yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateBanner(BannerDto bannerDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Banners", bannerDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateBanner");
                return new ErrorResult("Veri güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteBanner(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Banners/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteBanner");
                return new ErrorResult("Veri silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
