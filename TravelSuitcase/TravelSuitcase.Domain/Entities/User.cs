using TravelSuitcase.Domain.Common.Entities;
using TravelSuitcase.Domain.Common.Exceptions;
using TravelSuitcase.Domain.Common.Exceptions.User;
using TravelSuitcase.Domain.Common.Validators;

namespace TravelSuitcase.Domain.Entities
{
    public class User : AuditableEntity
    {
        private static readonly EmailValidator _EmailValidator = new();
        private static readonly LoginValidator _LoginValidator = new();
        private static readonly PasswordValidator _PasswordValidator = new();
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; private set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
        public ICollection<SuitCase> SuitCases { get; set; } = new HashSet<SuitCase>();

        public User(int userId, string email, string login, string password)
        {
            Id = userId;
            SetEmail(email);
            SetLogin(login);
            SetPassword(password);
        }

        public User(string login, string email, string password, byte[] passwordSalt)
        {
            SetEmail(email);
            SetLogin(login);
            SetPassword(password);
            SetPasswordSalt(passwordSalt);
        }

        private void SetEmail(string email)
        {
            if (!_EmailValidator.IsValid(email))
            {
                throw new IllegalOperationException<EmailExceptions>(EmailExceptions.Invalid);
            }

            Email = email;
        }

        private void SetLogin(string login)
        {
            if (!_LoginValidator.IsValid(login))
            {
                throw new IllegalOperationException<LoginExceptions>(LoginExceptions.Invalid);
            }

            Login = login;
        }

        private void SetPassword(string password)
        {
            if (!_PasswordValidator.IsValid(password))
            {
                throw new IllegalOperationException<PasswordException>(PasswordException.Required);
            }

            Password = password;
        }

        private void SetPasswordSalt(byte[] passwordSalt)
        {
            if (passwordSalt is null || passwordSalt.Length == 0)
            {
                throw new IllegalOperationException<PasswordSaltException>(PasswordSaltException.Required);
            }

            PasswordSalt = passwordSalt;
        }
    }
}