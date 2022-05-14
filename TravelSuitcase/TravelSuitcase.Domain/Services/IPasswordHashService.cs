namespace TravelSuitcase.Domain.Services
{
    public interface IPasswordHashService
    {
        string ComputeHash(string password, byte[] salt);

        byte[] GenerateSalt();

        bool IsHashEqual(string attemptedPassword, string hash, byte[] salt);
    }
}