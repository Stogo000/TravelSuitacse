using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace TravelSuitcase.Domain.Common.DTOs
{
    public record GenerateTokenDTO
    (
        [Required] JwtSecurityToken JwtSecurityToken,
        [Required] string RefreshToken,
        [Required] DateTime RefreshTokenExpireDate
    );
}