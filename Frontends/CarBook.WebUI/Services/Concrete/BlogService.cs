using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.BlogDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly ILogger<BlogService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public BlogService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<BlogService> logger, IApiResponseHandler apiResponseHandler)
        {
            _client = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<IEnumerable<BlogDto>>> GetAllBlogs()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Blogs/GetBlogsWithAuthor");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<BlogDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllBlogs");
                return new ErrorDataResult<IEnumerable<BlogDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<BlogDto>> GetBlogById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Blogs/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<BlogDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetBlogById");
                return new ErrorDataResult<BlogDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<BlogDto>>> GetLatest3Blogs()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Blogs/GetLatest3BlogsWithAuthor");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<BlogDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetLatest3Blogs");
                return new ErrorDataResult<IEnumerable<BlogDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Blogs", createBlogDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateBlog");
                return new ErrorResult("Veriler eklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteBlog(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"{_baseUrl}/api/Blogs/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteBlog");
                return new ErrorResult("Veriler silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
        public async Task<IResult> UpdateBlog(BlogDto blogDto)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Blogs", blogDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateBlog");
                return new ErrorResult("Veriler güncellenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
