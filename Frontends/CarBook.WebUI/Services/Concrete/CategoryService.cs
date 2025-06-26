using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.CategoryDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<CategoryService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public CategoryService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CategoryService> logger, IApiResponseHandler apiResponseHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }
        public async Task<IDataResult<IEnumerable<CategoryDto>>> GetAllCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Categories");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<CategoryDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllCategories");
                return new ErrorDataResult<IEnumerable<CategoryDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Categories", createCategoryDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateCategory");
                return new ErrorResult("Veriler eklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteCategory(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Categories/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteCategory");
                return new ErrorResult("Veriler silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }


        public async Task<IDataResult<CategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Categories/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<CategoryDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCategoryById");
                return new ErrorDataResult<CategoryDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Categories", categoryDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteCategory");
                return new ErrorResult("Veriler güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
