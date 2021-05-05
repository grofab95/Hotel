using Hotel.Domain.Entities.Common;
using Hotel.Domain.Utilities.Models;
using Hotel.Domain.Validators;

namespace Hotel.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public Token Token { get; private set; }

        private User() { }

        public User(string name, string email, Password password)
        {
            UserValidators.ValidIfNameExist(name);
            UserValidators.ValidIfEmailExist(email);
            UserValidators.ValidIfPasswordExist(password);
            EmailValidators.ValidEmail(email);
            Name = name;
            Email = email;
            PasswordHash = password.PasswordHash;
            PasswordSalt = password.PasswordSalt;
            Token = new Token(this);
        }

        public bool IsPasswordValid(string password)
        {
            return new Password(PasswordHash, PasswordSalt).Equals(password);
        }
    }
}
