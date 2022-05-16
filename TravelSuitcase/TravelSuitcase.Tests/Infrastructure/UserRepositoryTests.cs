using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.Repositories.Users;
using TravelSuitcase.Domain.Services;
using Xunit;
using Xunit.Sdk;

namespace TravelSuitcase.Tests.Infrastructure
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task CreateUser_ValidData_ShouldSuccess()
        {
            // Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();

            IPasswordHashService passwordHashService = Substitute.For<IPasswordHashService>();
            passwordHashService.ComputeHash(Arg.Any<string>(), Arg.Any<byte[]>())
                .Returns(Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword));
            passwordHashService.GenerateSalt()
                .Returns(Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword));

            UsersProcessor usersProcessor = new(userRepository, passwordHashService);

            byte[] expectedPasswordHash = Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword);
            byte[] expectedPasswordSalt = Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword);

            CreateUserDTO createUserDTO = new(
                UsersTestConstants.ValidEmail,
                UsersTestConstants.ValidPassword);

            // Act
            User user = await usersProcessor.CreateAsync(createUserDTO, Arg.Any<CancellationToken>());

            // Assert
            Assert.NotNull(user);
            Assert.Equal(createUserDTO.EmailAddress, user.Email);
            Assert.Equal(expectedPasswordHash, user.PasswordHash);
            Assert.Equal(expectedPasswordSalt, user.PasswordSalt);
        }

        [Fact]
        public async Task CreateUser_DuplicatedUser_ShouldThrowAnException()
        {
            // Arrange
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            userRepository.EmailExistsAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(true);

            IPasswordHashService passwordHashService = Substitute.For<IPasswordHashService>();

            UsersProcessor usersProcessor = new(userRepository, passwordHashService);

            CreateUserDTO createUserDTO = new(
                UsersTestConstants.ValidEmail,
                UsersTestConstants.ValidPassword);

            // Act
            try
            {
                await usersProcessor.CreateAsync(createUserDTO);

                throw new XunitException("Exception expected.");
            }
            catch (IllegalOperationException<EmailResult> ex)
            {
                // Assert
                Assert.Equal(EmailResult.AlreadyExists, ex.Status);
            }
        }
    }
}