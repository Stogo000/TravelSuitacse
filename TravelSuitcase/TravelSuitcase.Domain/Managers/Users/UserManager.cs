using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.User;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.Repositories.Users;
using TravelSuitcase.Domain.Services;

namespace TravelSuitcase.Domain.Managers.Users
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;

        public UserManager(
            IUserRepository userRepository,
            IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
        }

        public virtual async Task<User> CreateAsync(CreateUserDTO userDto, CancellationToken cancellationToken = default)
        {

            if (userDto is null)
            {
                throw new IllegalOperationException<UserDtoException>(UserDtoException.CannotBeNull);
            }

            if (await _userRepository.EmailExistsAsync(userDto.EmailAddress, cancellationToken))
            {
                throw new IllegalOperationException<EmailExceptions>(EmailExceptions.AlreadyExists);
            }

            byte[] newSalt = _passwordHashService.GenerateSalt();

            string hashedPassword = _passwordHashService.ComputeHash(userDto.Password, newSalt);

            User user = new(userDto.Login, userDto.EmailAddress, hashedPassword, newSalt);

            await _userRepository.AddAsync(user, cancellationToken);

            return user;
        }
    }
}