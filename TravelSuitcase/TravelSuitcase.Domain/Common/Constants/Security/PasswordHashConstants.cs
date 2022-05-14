namespace TravelSuitcase.Domain.Common.Constants.Security
{
    public static class PasswordHashConstants
    {
        public const int HashLength = 64;

        public const int SaltLength = 256 / 8;

        public const int Iterations = 20000;
    }
}