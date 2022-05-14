using System.Security.Cryptography;
using TravelSuitcase.Domain.Common.Constants.Security;
using TravelSuitcase.Domain.Services;

namespace TravelSuitcase.Infrastructure.Services
{
    public class PasswordHashServices : IPasswordHashService
    {
        public string ComputeHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(new Rfc2898DeriveBytes(
                    password,
                    salt,
                    PasswordHashConstants.Iterations)
                .GetBytes(PasswordHashConstants.HashLength));
        }

        public byte[] GenerateSalt()
        {
            byte[] bytes = new byte[PasswordHashConstants.SaltLength];
            RandomNumberGenerator.Create().GetBytes(bytes);

            return bytes;
        }

        public bool IsHashEqual(string attemptedPassword, string hash, byte[] salt)
        {
            string newHash = ComputeHash(attemptedPassword, salt);

            return hash.Equals(newHash);
        }
    }
}