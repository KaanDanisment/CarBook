using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.AuthorDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<AuthorService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IDataResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Api/Authors");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<AuthorDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllAuthors");
                return new ErrorDataResult<IEnumerable<AuthorDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Api/Authors", createAuthorDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateAuthor");
                return new ErrorResult("Veri eklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteAuthor(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/Api/Authors/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteAuthor");
                return new ErrorResult("Veri silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }




        public async Task<IDataResult<AuthorDto>> GetAuthorById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/Api/Authors/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<AuthorDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAuthorById");
                return new ErrorDataResult<AuthorDto>("Veri yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateAuthor(AuthorDto authorDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/Api/Authors", authorDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateAuthor");
                return new ErrorResult("Veri güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
