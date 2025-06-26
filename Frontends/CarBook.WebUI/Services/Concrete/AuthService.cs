using CarBook.Adapters.Handlers.ResponseHandlers.Abstracts;
using CarBook.Application.Common.Results.Abstracts;
using CarBook.Application.Common.Results.Concretes;
using CarBook.Dto.AppUserDtos;
using CarBook.Dto.AuthenticationDtos;
using CarBook.WebUI.Services.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly ILogger<AuthService> _logger;
        private readonly IApiResponseHandler _apiResponseHandler;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AuthService> logger, IApiResponseHandler apiResponseHandler)
        {
            _client = httpClientFactory.CreateClient();
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
            _apiResponseHandler = apiResponseHandler;
        }

        public async Task<IDataResult<JwtResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Auth/login", loginDto);
                var result = await _apiResponseHandler.HandleApiResponse<JwtResponseDto>(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: Login");
                return new ErrorDataResult<JwtResponseDto>("Giriş işlemi sırasında bir hata oluştu, lütfen daha sonra tekrar deneyin.", "InternalServerError");
            }
        }

        public async Task<IResult> Register(RegisterDto registerDto)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"{_baseUrl}/api/Auth/register", registerDto);
                var result = await _apiResponseHandler.HandleApiResponse(response);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API Call Error: Register");
                return new ErrorResult("Kayıt işlemi sırasında bir hata oluştu, lütfen daha sonra tekrar deneyin.", "InternalServerError");
            }
        }
    }
}
