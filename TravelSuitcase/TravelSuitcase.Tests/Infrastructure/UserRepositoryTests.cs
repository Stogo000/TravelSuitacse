using NSubstitute;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelSuitcase.Domain.Common.DTOs;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.User;
using TravelSuitcase.Domain.Entities;
using TravelSuitcase.Domain.Services;
using TravelSuitcase.Infrastructure.Persistence;
using TravelSuitcase.Infrastructure.Persistence.Repositories.Users;
using TravelSuitcase.Tests.TestData.Constants;
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
            IPasswordHashService passwordHashService = Substitute.For<IPasswordHashService>();
            ISecurityService securityService = Substitute.For<ISecurityService>();
            ApplicationDbContext applicationDbContext = Substitute.For<ApplicationDbContext>();

            passwordHashService.ComputeHash(Arg.Any<string>(), Arg.Any<byte[]>())
                .Returns(UsersTestConstants.ValidPassword);
            passwordHashService.GenerateSalt()
                .Returns(Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword));

            string expectedPasswordHash = UsersTestConstants.ValidPassword;
            byte[] expectedPasswordSalt = Encoding.UTF8.GetBytes(UsersTestConstants.ValidPassword);

            CreateUserDTO createUserDTO = new(
                UsersTestConstants.ValidEmail,
                UsersTestConstants.ValidLogin,
                UsersTestConstants.ValidPassword);

            UserRepository userRepository = new(applicationDbContext, passwordHashService, securityService);
            // Act
            User user = await userRepository.CreateAsync(createUserDTO, Arg.Any<CancellationToken>());

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
            IPasswordHashService passwordHashService = Substitute.For<IPasswordHashService>();
            ISecurityService securityService = Substitute.For<ISecurityService>();
            ApplicationDbContext applicationDbContext = Substitute.For<ApplicationDbContext>();

            UserRepository userRepository = new(applicationDbContext, passwordHashService, securityService);

            //userRepository.EmailExistsAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);

            CreateUserDTO createUserDTO = new(
                UsersTestConstants.ValidEmail,
                UsersTestConstants.ValidLogin,
                UsersTestConstants.ValidPassword);

            // Act
            try
            {
                await userRepository.CreateAsync(createUserDTO);

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