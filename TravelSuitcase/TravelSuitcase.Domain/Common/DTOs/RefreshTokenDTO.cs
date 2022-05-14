using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace TravelSuitcase.Domain.Common.DTOs
{
    public record RefreshTokenDTO
    (
        [Required] JwtSecurityToken JwtSecurityToken,
        [Required] string RefreshToken
    );
}