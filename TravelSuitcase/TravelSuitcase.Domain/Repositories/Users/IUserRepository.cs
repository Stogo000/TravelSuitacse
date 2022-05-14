using System.IdentityModel.Tokens.Jwt;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Entities;

namespace TravelSuitcase.Domain.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);

        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> CreateAsync(CreateUserDTO user, CancellationToken cancellationToken = default);

        Task<bool> LoginExistsAsync(string login, CancellationToken cancellationToken = default);

        Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default);

        Task<RefreshTokenDTO> LogInAsync(LoginDTO loginDto,
            CancellationToken cancellationToken = default);

        Task<RefreshTokenDTO> RefreshToken(string refreshToken,
            CancellationToken cancellationToken = default);
    }
}