using Hotel.Domain.Excetions;

namespace Hotel.Domain.Validators
{
    public class RoomGuestValidators
    {
        public static void ValidName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                throw new HotelException("Nazwa gościa jest wymagana.");
        }

        public static void ValidPriceForStay(decimal? priceForStay)
        {
            if (priceForStay == null)
                return;

            if (priceForStay < 0)
                throw new HotelException("Cena za pobyt nie może być ujemna.");
        }
    }
}
