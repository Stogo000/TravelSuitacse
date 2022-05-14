using System.Text.RegularExpressions;
using TravelSuitcase.Domain.Common.Constants.Entities;
using TravelSuitcase.Domain.Common.Validators.Interfaces;

namespace TravelSuitcase.Domain.Common.Validators
{
    public class EmailValidator : IValidator<string>
    {
        public bool IsValid(string value)
        {
            return IsNotEmpty(value)
                   && IsLengthInRange(value)
                   && IsRegexValid(value);
        }

        private bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        private bool IsLengthInRange(string value)
        {
            return value.Length is > EmailConstants.Min and < EmailConstants.Max;
        }

        private bool IsRegexValid(string value)
        {
            return Regex.IsMatch(value, EmailConstants.Regex);
        }
    }
}