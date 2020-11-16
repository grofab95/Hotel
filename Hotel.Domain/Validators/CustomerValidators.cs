using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Validators
{
    public class CustomerValidators
    {
        public static void ValidIfFirstNameExist(string firstName)
        {
            if (firstName.IsNotExist())
                throw new MissingValueException("Imię jest wymagane.");
        }

        public static void ValidIfLastNameExist(string lastName)
        {
            if (lastName.IsNotExist())
                throw new MissingValueException("Nazwisko jest wymagane.");
        }

        public static void ValidIfFirstAndLastNameAreTheSame(string firstName, string lastName)
        {
            if (firstName.IsLike(lastName))
                throw new HotelException("Imię i nazwisko nie mogą byc takie same.");
        }
    }
}
