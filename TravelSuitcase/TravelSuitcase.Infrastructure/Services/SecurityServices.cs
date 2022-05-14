using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelSuitcase.Domain.Common.Constants.Security;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Providers;
using TravelSuitcase.Domain.Services;

namespace TravelSuitcase.Infrastructure.Services
{
    public class SecurityServices : ISecurityService
    {
        private readonly IJWTConfigurationProvider _jwtConfigurationProvider;

        public SecurityServices(IJWTConfigurationProvider jwtConfigurationProvider)
        {
            _jwtConfigurationProvider = jwtConfigurationProvider;
        }

        public GenerateTokenDTO GenerateAccessToken(string login)
        {
            Claim[] userclaim = new[]
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, RoleEnums.User.ToString()),
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfigurationProvider.GetJWTOptions().SecretKey));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(
                issuer: _jwtConfigurationProvider.GetJWTOptions().Issuer,
                audience: _jwtConfigurationProvider.GetJWTOptions().Audience,
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );
            GenerateTokenDTO generateTokenDto = new(token, Guid.NewGuid().ToString(), DateTime.Now.AddDays(1));
            return generateTokenDto;
        }

        public RefreshTokenDTO GenerateRefreshToken(string login)
        {
            GenerateTokenDTO accessToken = GenerateAccessToken(login);
            RefreshTokenDTO refreshTokenDto = new(accessToken.JwtSecurityToken, accessToken.RefreshToken);
            return refreshTokenDto;
        }
    }
}