using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using System;
using System.Linq;

namespace Hotel.Domain.Validators;

public class ReservationValidators
{
    public static void ValidIfReservationRoomExistInReservation(Reservation reservation, ReservationRoom reservationRoom)
    {
        if (!reservation.ReservationRooms?.Any(x => x.Id == reservationRoom.Id) ?? true)
            throw new HotelException($"W tej rezerwacji nie istnieje pokój rezerwacyjny o id {reservationRoom.Id}.");
    }

    public static void ValidDates(DateTime checkIn, DateTime checkOut)
    {
        if (checkIn < DateTime.Now)
            throw new HotelException("Nie można stworzyć rezerwacji w przeszłości.");

        if (checkIn > checkOut)
            throw new HotelException("Date zameldowania nie może byc późniejsza od daty wymeldowania.");
    }
}