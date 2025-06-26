using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.ContactDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<ContactService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IResult> CreateContact(CreateContactDto createContactDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Contacts", createContactDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateContact");
                return new ErrorResult("Veriler eklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<ContactDto>>> GetAllContacts()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Contacts");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<ContactDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllContacts");
                return new ErrorDataResult<IEnumerable<ContactDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
