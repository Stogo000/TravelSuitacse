using System.ComponentModel.DataAnnotations;

namespace TravelSuitcase.Domain.Common.DTOs
{
    public record TokenResponseDTO
    (
        [Required] string Token,
        [Required] string RefreshToken
    );
}