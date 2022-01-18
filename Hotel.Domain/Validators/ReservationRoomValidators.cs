using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;

namespace Hotel.Domain.Validators;

public class ReservationRoomValidators
{
    public static void ValidBookingAmount(Room room, int bookingAmount)
    {
        if (bookingAmount <= 0)
            throw new HotelException("Zarezerwowana ilość w pokoju musi być większa od zera.");

        if (bookingAmount > room.PeopleCapacity)
            throw new HotelException("Ilość osób w rezerwacji pokoju nie może większa niż możliwa ilość osób w pokoju.");
    }
}