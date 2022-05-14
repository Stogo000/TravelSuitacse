using System.ComponentModel.DataAnnotations;

namespace TravelSuitcase.Application.Queries.Users.GetUsers
{
    public record UserDTO
    (
        [Required] string EmailAddress,
        [Required] string Login
    );
}