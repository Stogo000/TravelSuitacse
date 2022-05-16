using TravelSuitcase.Domain.Common.Validators;
using Xunit;

namespace TravelSuitcase.Tests.Domain.Validators
{
    public class PasswordValidatorTests
    {
        [Theory]
        [InlineData("Alaa1234!")]
        [InlineData("SuperTajneHaslo123!")]
        [InlineData("zaq12@WSX")]
        public void Validate_ValidPassword_ShouldSuccess(string password)
        {
            // Arrange and act
            bool isValid = new PasswordValidator().IsValid(password);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("superhaslo")]
        [InlineData("Supertajnehaslo")]
        [InlineData("SuperTajneHaslo!")]
        [InlineData("aaaaaa")]
        public void Validate_InvalidLogin_ShouldFail(string password)
        {
            // Arrange and act
            bool isValid = new PasswordValidator().IsValid(password);

            // Assert
            Assert.False(isValid);
        }
    }
}