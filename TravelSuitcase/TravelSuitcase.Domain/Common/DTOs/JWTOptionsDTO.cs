using System.ComponentModel.DataAnnotations;

namespace TravelSuitcase.Domain.Common.DTOs
{
    public record JWTOptionsDTO
    (
        [Required] string Issuer,
        [Required] string Audience,
        [Required] string SecretKey
    );
}