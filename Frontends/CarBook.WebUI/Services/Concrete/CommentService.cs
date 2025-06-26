using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.CommentDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private IApiResponseHandler _apiResponseHandler;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IApiResponseHandler apiResponseHandler, ILogger<CommentService> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiResponseHandler = apiResponseHandler;
            _logger = logger;
        }

        public async Task<IDataResult<IEnumerable<CommentDto>>> GetCommentsByBlogId(int blogId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Comments/GetCommentsByBlogId/{blogId}");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<CommentDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCommentsByBlogId");
                return new ErrorDataResult<IEnumerable<CommentDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteComment(int commentId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Comments/{commentId}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteComment");
                return new ErrorResult("Veriler silinirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<CommentsCountDto>> GetCommentsCountByBlogId(int blogId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/Comments/GetCommentsCountByBlogId/{blogId}");
                var result = await _apiResponseHandler.HandleApiResponse<CommentsCountDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetCommentsCountByBlogId");
                return new ErrorDataResult<CommentsCountDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> CreateComment(CreateCommentDto createCommentDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Comments", createCommentDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateComment");
                return new ErrorResult("Yorum oluşturulurken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
