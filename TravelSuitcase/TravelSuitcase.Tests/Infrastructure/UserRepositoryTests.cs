using NSubstitute;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.User;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.Managers.Users;
using TravelSuitcase.Domain.Repositories.Users;
using TravelSuitcase.Domain.Services;
using TravelSuitcase.Tests.TestData.Constants;
using Xunit;
using Xunit.Sdk;

namespace TravelSuitcase.Tests.Infrastructure
{
    public class UserRepositoryTests
    {
        private readonly UserManager _userManager;
        private IUserRepository userRepository = Substitute.For<IUserRepository>();
        private readonly IPasswordHashService passwordHashService = Substitute.For<IPasswordHashService>();

        public UserRepositoryTests()
        {
            _userManager = new UserManager(userRepository, passwordHashService);
        }

        [Fact]
        public async Task CreateUser_ValidData_ShouldSuccess()
        {
            // Arrange
            passwordHashService.ComputeHash(Arg.Any<string>(), Arg.Any<byte[]>())
                .Returns(UsersTestConstants.ValidPassword);
            passwordHashService.GenerateSalt()
                .Returns(Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword));

            string expectedPasswordHash = UsersTestConstants.ValidPassword;
            byte[] expectedPasswordSalt = Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword);

            CreateUserDTO createUserDTO = new(
                UsersTestConstants.ValidEmail,
                UsersTestConstants.ValidLogin,
                UsersTestConstants.ValidPassword); ;
            // Act
            User user = await _userManager.CreateAsync(createUserDTO, Arg.Any<CancellationToken>());

            // Assert
            Assert.NotNull(user);
            Assert.Equal(createUserDTO.EmailAddress, user.Email);
            Assert.Equal(createUserDTO.Login, user.Login);
            Assert.Equal(expectedPasswordHash, user.Password);
            Assert.Equal(expectedPasswordSalt, user.PasswordSalt);
        }

        [Fact]
        public async Task CreateUser_DuplicatedUser_ShouldThrowAnException()
        {
            // Arrange
            userRepository.EmailExistsAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(true);

            CreateUserDTO createUserDTO = new(
                UsersTestConstants.ValidEmail,
                UsersTestConstants.ValidLogin,
                UsersTestConstants.ValidPassword);

            // Act
            try
            {
                await _userManager.CreateAsync(createUserDTO);

                throw new XunitException("Exception expected.");
            }
            catch (IllegalOperationException<EmailExceptions> ex)
            {
                // Assert
                Assert.Equal(EmailExceptions.AlreadyExists, ex.Status);
            }
        }
    }
}