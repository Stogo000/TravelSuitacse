using TravelSuitcase.Domain.Common.Validators;
using Xunit;

namespace TravelSuitcase.Tests.Domain.Validators
{
    public class LoginValidatorTests
    {
        [Theory]
        [InlineData("Ala123")]
        [InlineData("Michal!")]
        [InlineData("michal 123")]
        public void Validate_ValidLogin_ShouldSuccess(string login)
        {
            // Arrange and act
            bool isValid = new LoginValidator().IsValid(login);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("@")]
        [InlineData(".")]
        [InlineData("a@.aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Validate_InvalidLogin_ShouldFail(string login)
        {
            // Arrange and act
            bool isValid = new LoginValidator().IsValid(login);

            // Assert
            Assert.False(isValid);
        }
    }
}