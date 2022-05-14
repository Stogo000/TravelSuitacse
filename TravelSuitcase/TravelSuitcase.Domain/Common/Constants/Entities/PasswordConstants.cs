namespace TravelSuitcase.Domain.Common.Constants.Entities
{
    public static class PasswordConstants
    {
        public const int MaxLength = 250;

        public const int MinLength = 8;

        public const string Regex = @"^((?=.*$)(?!.*[\s]))(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$";
    }
}