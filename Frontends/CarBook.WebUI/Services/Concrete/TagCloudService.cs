using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.TagCloudDtos;
using CarBook.WebUI.Services.Abstracts;

namespace CarBook.WebUI.Services.Concrete
{
    public class TagCloudService : ITagCloudService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<TagCloudService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public TagCloudService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TagCloudService> logger, IApiResponseHandler apiResponseHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<IEnumerable<TagCloudDto>>> GetTagCloudsByBlogId(int blogId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/TagClouds/GetTagCloudsByBlogId/{blogId}");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<TagCloudDto>>(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetTagCloudsByBlogId");
                return new ErrorDataResult<IEnumerable<TagCloudDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
