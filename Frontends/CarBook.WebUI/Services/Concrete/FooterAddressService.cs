using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.FooterAddressDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class FooterAddressService : IFooterAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<FooterAddressService> _logger;

        public FooterAddressService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<FooterAddressService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IResult> CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/FooterAddresses", createFooterAddressDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: CreateFooterAddress");
                return new ErrorResult("Veriler eklenirken bir hata oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteFooterAddress(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/FooterAddresses/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: DeleteFooterAddress");
                return new ErrorResult("Veriler silinirken bir hata oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<FooterAddressDto>>> GetAllFooterAddresses()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/FooterAddresses");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<FooterAddressDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetAllFooterAddresses");
                return new ErrorDataResult<IEnumerable<FooterAddressDto>>("Veriler alınırken bir hata oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<FooterAddressDto>> GetFooterAddressById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/FooterAddresses/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<FooterAddressDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: GetFooterAddressById");
                return new ErrorDataResult<FooterAddressDto>("Veriler alınırken bir hata oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateFooterAddress(FooterAddressDto footerAddressDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/FooterAddresses", footerAddressDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Api Call Error: UpdateFooterAddress");
                return new ErrorResult("Veriler güncellenirken bir hata oluştu lütfen daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
