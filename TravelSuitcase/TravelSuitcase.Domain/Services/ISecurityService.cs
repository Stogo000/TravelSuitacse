using TravelSuitcase.Domain.Common.DTOs;

namespace TravelSuitcase.Domain.Services
{
    public interface ISecurityService
    {
        GenerateTokenDTO GenerateAccessToken(string login);

        RefreshTokenDTO GenerateRefreshToken(string refreshToken);
    }
}