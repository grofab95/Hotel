using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;
using Hotel.Domain.Utilities.Models;

namespace Hotel.Domain.Validators
{
    class UserValidators
    {
        public static void ValidIfNameExist(string name)
        {
            if (name.IsNotExist())
                throw new MissingValueException("Imię jest wymagane.");
        }

        public static void ValidIfSurnameExist(string surname)
        {
            if (surname.IsNotExist())
                throw new MissingValueException("Nazwisko jest wymagane.");
        }

        public static void ValidIfEmailExist(string email)
        {
            if (email.IsNotExist())
                throw new MissingValueException("E-mail jest wymagana.");
        }

        public static void ValidIfPasswordExist(Password password)
        {
            if (password == null)
                throw new MissingValueException("Hasło jest wymagane");
        }
    }
}
