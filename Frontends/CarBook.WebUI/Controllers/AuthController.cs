using CarBook.Dto.AppUserDtos;
using CarBook.WebUI.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarBook.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(registerDto);
            }
            TempData["SuccessMessage"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(loginDto);
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(result.Data.Token);
            var identity = new ClaimsIdentity(jwtToken.Claims, "CarBookScheme");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CarBookScheme", principal);

            TempData["SuccessMessage"] = "Giriş başarılı! Hoş geldiniz.";
            return RedirectToAction("Index", "Default");
        }
    }
}
