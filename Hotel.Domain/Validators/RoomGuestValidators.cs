using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Validators
{
    public class RoomGuestValidators
    {
        public static void ValidIfNameExist(string name)
        {
            if (name.IsNotExist())
                throw new HotelException("Nazwa gościa jest wymagana.");
        }

        public static void ValidPriceForStay(decimal priceForStay)
        {
            if (priceForStay < 0)
                throw new HotelException("Cena za pobyt nie może być ujemna.");
        }
    }
}
