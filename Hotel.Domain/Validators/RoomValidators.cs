using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Validators
{
    public class RoomValidators
    {
        public static void ValidIfAreaExist(Area area)
        {
            if (area == null)
                throw new MissingValueException("Obszar jest wymagany.");
        }

        public static void ValidIfNameExist(string name)
        {
            if (name.IsNotExist())
                throw new MissingValueException("Nazwa pokoju jest wymagana.");
        }

        public static void ValidIfPeopleCapacityIsPositive(int peopleCapacity)
        {
            if (peopleCapacity <= 0)
                throw new HotelException("Ilość musi być większa od zera.");
        }
    }
}
