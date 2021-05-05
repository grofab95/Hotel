using Hotel.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Hotel.Domain.Validators
{
    public class EmailValidators
    {
        public static void ValidEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                throw new InvalidEmailException($"E-mail {email} jest nieprawidłowy.");
        }
    }
}
