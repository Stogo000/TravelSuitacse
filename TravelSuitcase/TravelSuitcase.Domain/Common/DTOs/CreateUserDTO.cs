using System.ComponentModel.DataAnnotations;

namespace TravelSuitcase.Domain.Common.DTOs
{
    public record CreateUserDTO(
        [Required] string EmailAddress,
        [Required] string Login,
        [Required] string Password
    );
}