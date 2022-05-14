using TravelSuitcase.Domain.Common.Constants.Entities;
using TravelSuitcase.Domain.Common.Validators.Interfaces;

namespace TravelSuitcase.Domain.Common.Validators
{
    public class LoginValidator : IValidator<string>
    {
        public bool IsValid(string value)
        {
            return IsNotEmpty(value)
                && IsLengthInRange(value);
        }

        private bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        private bool IsLengthInRange(string value)
        {
            return value.Length is > LoginConstants.Min and < LoginConstants.Max;
        }
    }
}