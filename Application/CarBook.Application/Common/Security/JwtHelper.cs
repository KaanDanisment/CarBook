using CarBook.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarBook.Application.Common.Security
{
    public class JwtHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AccessToken CreateToken(AppUser appUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenOptions:SecurityKey"]));
            var siginCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.AppUserId.ToString()),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.Role, appUser.AppRole.AppRoleName)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["TokenOptions:Issuer"],
                audience: _configuration["TokenOptions:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                notBefore: DateTime.Now,
                signingCredentials: siginCredentials
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var writtenToken = tokenHandler.WriteToken(token);

            return new AccessToken()
            {
                Token = writtenToken,
                Expiration = token.ValidTo
            };
        }
    }
}
