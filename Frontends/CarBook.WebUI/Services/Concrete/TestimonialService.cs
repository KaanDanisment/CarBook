using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.TestimonialDtos;
using CarBook.WebUI.Services.Abstracts;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class TestimonialService : ITestimonialService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly ILogger<TestimonialService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public TestimonialService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TestimonialService> logger, IApiResponseHandler apiResponseHandler)
        {
            _client = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Testimonials", createTestimonialDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: CreateTestimonial");
                return new ErrorResult("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> DeleteTestimonial(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"{_baseUrl}/api/Testimonials/{id}");
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: DeleteTestimonial");
                return new ErrorResult("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<IEnumerable<TestimonialDto>>> GetAllTestimonials()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Testimonials");
                var result = await _apiResponseHandler.HandleApiResponse<IEnumerable<TestimonialDto>>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetAllTestimonials");
                return new ErrorDataResult<IEnumerable<TestimonialDto>>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IDataResult<TestimonialDto>> GetTestimonialById(int id)
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/api/Testimonials/{id}");
                var result = await _apiResponseHandler.HandleApiResponse<TestimonialDto>(response);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "API Call Error: GetTestimonialById");
                return new ErrorDataResult<TestimonialDto>("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }

        public async Task<IResult> UpdateTestimonial(TestimonialDto testimonialDto)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"{_baseUrl}/api/Testimonials", testimonialDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: UpdateTestimonial");
                return new ErrorResult("Veriler yüklenirken bir hata oluştu daha sonra tekrar deneyin", "InternalServerError");
            }
        }
    }
}
