using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;
using System;

namespace Hotel.Domain.Utilities.Models
{
    public class Password : IEquatable<string>
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public Password(string password)
        {
            if (password.IsNotExist())
                throw new MissingValueException($"Hasło jest wymagane");

            PasswordHashUtility.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public Password(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public bool Equals(string password)
        {
            return PasswordHashUtility.VerifyPasswordHash(password, PasswordHash, PasswordSalt);
        }
    }
}
