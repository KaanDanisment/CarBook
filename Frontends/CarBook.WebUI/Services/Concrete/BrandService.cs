using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.BrandDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<BrandService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public BrandService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<BrandService> logger, IApiResponseHandler apiResponseHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Brands");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<BrandDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllBrands");
                return new ErrorDataResult<IEnumerable<BrandDto>>("Markalar yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Brands", createBrandDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateBrand");
                return new ErrorResult("Marka eklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<BrandDto>> GetBrandById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Brands/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<BrandDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetBrandById");
                return new ErrorDataResult<BrandDto>("Marka yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateBrand(BrandDto brandDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Brands", brandDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateBrand");
                return new ErrorResult("Marka güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IResult> DeleteBrand(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Brands/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteBrand");
                return new ErrorResult("Marka silinirken hata oluştu daha sonra tekrar deneyin", "InternalServerError");

            }
        }
    }
}
