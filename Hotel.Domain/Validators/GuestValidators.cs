using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Validators;

public class GuestValidators
{
    public static void ValidIfNameExist(string name)
    {
        if (name.IsNotExist())
            throw new HotelException("Nazwa gościa jest wymagana.");
    }

    public static void ValidPrice(decimal priceForStay)
    {
        if (priceForStay < 0)
            throw new HotelException("Cena nie może być ujemna.");
    }
}