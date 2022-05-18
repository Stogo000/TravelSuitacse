using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.Security;
using TravelSuitcase.Domain.Common.Exceptions.User;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.Repositories.Users;
using TravelSuitcase.Domain.Services;

namespace TravelSuitcase.Infrastructure.Persistence.Repositories.Users
{
    public class UserRepository : MainRepository<User>, IUserRepository
    {
        private readonly IPasswordHashService _passwordHashService;
        private readonly ISecurityService _securityService;

        public UserRepository(ApplicationDbContext dbContext, IPasswordHashService passwordHashService, ISecurityService securityService)
            : base(dbContext)
        {
            _passwordHashService = passwordHashService;
            _securityService = securityService;
        }

        public virtual async Task<User> CreateAsync(CreateUserDTO userDto, CancellationToken cancellationToken = default)
        {
            if (userDto is null)
            {
                throw new IllegalOperationException<UserDtoException>(UserDtoException.CannotBeNull);
            }

            if (await EmailExistsAsync(userDto.EmailAddress, cancellationToken))
            {
                throw new IllegalOperationException<EmailExceptions>(EmailExceptions.AlreadyExists);
            }

            byte[] newSalt = _passwordHashService.GenerateSalt();

            string hashedPassword = _passwordHashService.ComputeHash(userDto.Password, newSalt);

            User user = new(userDto.Login,userDto.EmailAddress, hashedPassword, newSalt);

            await AddAsync(user, cancellationToken);

            return user;
        }

        public async Task<RefreshTokenDTO> LogInAsync(LoginDTO loginDto, CancellationToken cancellationToken = default)
        {
            if (!await LoginExistsAsync(loginDto.Login, cancellationToken))
            {
                throw new IllegalOperationException<LoginExceptions>(LoginExceptions.NotExists);
            }
            User user = await DbSet.FirstOrDefaultAsync(u => u.Login.Equals(loginDto.Login));
            bool isEqualHash = _passwordHashService.IsHashEqual(
                loginDto.Password,
                user.Password,
                user.PasswordSalt);
            if (!isEqualHash)
            {
                throw new IllegalOperationException<PasswordSaltException>(PasswordSaltException.InvalidPassword);
            }

            GenerateTokenDTO tuple = _securityService.GenerateAccessToken(user.Login);
            JwtSecurityToken token = tuple.JwtSecurityToken;
            string refreshToken = tuple.RefreshToken;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = tuple.RefreshTokenExpireDate;

            await UpdateAsync(user, cancellationToken);
            return new(token, refreshToken);
        }

        public async Task<RefreshTokenDTO> RefreshToken(string refreshToken, CancellationToken cancellationToken = default)
        {
            User user = await DbSet.FirstOrDefaultAsync(u => u.RefreshToken.Equals(refreshToken));
            if (user is null)
            {
                throw new IllegalOperationException<RefreshTokenException>(RefreshTokenException.NotExist);
            }
            RefreshTokenDTO refreshTokenDto = _securityService.GenerateRefreshToken(user.Login);
            return refreshTokenDto;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(x => x.Email.Equals(email), cancellationToken);
        }

        public async Task<bool> LoginExistsAsync(string login, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(x => x.Login.Equals(login), cancellationToken);
        }

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
        }

        public async Task<User> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Login.Equals(login), cancellationToken);
        }
    }
}