using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.PricingDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class PricingService : IPricingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<PricingService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public PricingService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<PricingService> logger, IApiResponseHandler apiResponseHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IResult> CreatePricing(CreatePricingDto createPricingDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Pricings", createPricingDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: CreatePricing");
                return new ErrorResult("Veriler eklenirken bir sorun oluştu lütfen daha sonra tekrar deneyin");
            }
        }

        public async Task<IResult> DeletePricing(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Pricings/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: DeletePrice");
                return new ErrorResult("Veriler silinirken bir sorun oluştu lütfen daha sonra tekrar deneyin");
            }
        }

        public async Task<IDataResult<IEnumerable<PricingDto>>> GetAllPricing()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Pricings");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<PricingDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetAllPricing");
                return new ErrorDataResult<IEnumerable<PricingDto>>("Veriler alınırken bir sorun oluştu lütfen daha sonra tekrar deneyin");
            }
        }

        public async Task<IDataResult<PricingDto>> GetPricingById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Pricings/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<PricingDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetPricingById");
                return new ErrorDataResult<PricingDto>("Veriler alınırken bir sorun oluştu lütfen daha sonra tekrar deneyin");
            }
        }

        public async Task<IResult> UpdadetePricing(PricingDto pricingDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Pricings", pricingDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: UpdadetePricing");
                return new ErrorResult("Veriler güncellenirken bir sorun oluştu lütfen daha sonra tekrar deneyin");
            }

        }
    }
}
