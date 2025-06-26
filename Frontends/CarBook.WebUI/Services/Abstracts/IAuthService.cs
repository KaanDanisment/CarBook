using CarBook.Application.Common.Results.Abstracts;
using CarBook.Dto.AppUserDtos;
using CarBook.Dto.AuthenticationDtos;
using IResult = CarBook.Application.Common.Results.Abstracts.IResult;

namespace CarBook.WebUI.Services.Abstracts
{
    public interface IAuthService
    {
        Task<IResult> Register(RegisterDto registerDto);
        Task<IDataResult<JwtResponseDto>> Login(LoginDto loginDto);
    }
}
