using TravelSuitcase.Domain.Common.Validators;
using Xunit;

namespace TravelSuitcase.Tests.Domain.Validators
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("ala@a.a")]
        [InlineData("ab@ab.ab")]
        [InlineData("ab!ab#@ab.ab")]
        [InlineData("abcd@abcd.abcd")]
        [InlineData("f!lIp@gmail.edu")]
        public void Validate_ValidEmail_ShouldSuccess(string email)
        {
            // Arrange and act
            bool isValid = new EmailValidator().IsValid(email);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("@")]
        [InlineData(".")]
        [InlineData("a@")]
        [InlineData("a@a")]
        [InlineData("a@.a")]
        [InlineData("a@a.")]
        [InlineData("@.a")]
        [InlineData("@a.")]
        public void Validate_InvalidEmail_ShouldFail(string email)
        {
            // Arrange and act
            bool isValid = new EmailValidator().IsValid(email);

            // Assert
            Assert.False(isValid);
        }
    }
}