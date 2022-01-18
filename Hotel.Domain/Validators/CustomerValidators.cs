using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Validators;

public class CustomerValidators
{
    public static void ValidIftNameExist(string name)
    {
        if (name.IsNotExist())
            throw new MissingValueException("Nazwa jest wymagana.");
    }
}