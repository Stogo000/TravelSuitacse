using System.ComponentModel.DataAnnotations;
using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Application.Queries.Users.GetUsers
{
    public record GetUsersResponseDTO
    (
        [Required] ICollection<User> Users
    );
}