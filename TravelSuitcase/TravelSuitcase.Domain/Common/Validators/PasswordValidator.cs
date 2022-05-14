using System.Text.RegularExpressions;
using TravelSuitcase.Domain.Common.Constants.Entities;
using TravelSuitcase.Domain.Common.Validators.Interfaces;

namespace TravelSuitcase.Domain.Common.Validators
{
    public class PasswordValidator : IValidator<string>
    {
        public bool IsValid(string value)
        {
            return !IsNullOrWhiteSpace(value)
                && IsLengthInRange(value)
                && IsRegexValid(value);
        }

        private bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        private bool IsLengthInRange(string value)
        {
            return value.Length is > PasswordConstants.MinLength and < PasswordConstants.MaxLength;
        }

        private bool IsRegexValid(string value)
        {
            return Regex.IsMatch(value, PasswordConstants.Regex);
        }
    }
}