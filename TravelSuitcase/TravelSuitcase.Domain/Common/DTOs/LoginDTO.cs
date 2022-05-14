using System.ComponentModel.DataAnnotations;

namespace TravelSuitcase.Domain.Common.DTOs
{
    public record LoginDTO
    (
        [Required] string Login,
        [Required] string Password
    );
}