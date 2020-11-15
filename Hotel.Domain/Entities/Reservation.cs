using Hotel.Domain.Entities.Common;
using Hotel.Domain.Excetions;
using Hotel.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities
{
    public class Reservation : Entity
    {
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
        public Customer Customer { get; private set; }
        public List<ReservationRoom> ReservationRooms { get; private set; }
        public bool WithBreakfest { get; private set; }

        public int PeopleAmount => ReservationRooms?.Sum(x => x.BookingAmount) ?? 0;

        private Reservation() 
        {
            ReservationRooms = new List<ReservationRoom>();
        }

        public static Result<Reservation> CreateReservation(Customer customer, DateTime checkIn, DateTime checkOut, bool withBreakfest)
        {
            try
            {
                if (customer == null)
                    throw new HotelException("Klient nie został określony.");

                if (checkIn < DateTime.Now)
                    throw new HotelException("Nie można stworzyć rezerwacji w przeszłości.");

                if (checkIn > checkOut)
                    throw new HotelException("Date zameldowania nie może byc późniejsza od daty wymeldowania.");
            }
            catch (Exception ex)
            {
                return Result<Reservation>.Fail(ex.Message);
            }

            return Result<Reservation>.Ok(new Reservation 
            {
                Customer = customer,
                CheckIn = checkIn,
                CheckOut = checkOut,
                WithBreakfest = withBreakfest
            });
        }

        public Result AddReservationRoom(Room room, int bookingAmount, int childrenAmount, bool isForNewlyweds)
        {
            if (ReservationRooms.Any(x => x.Room.Id == room.Id))
                return Result.Fail($"Pokój {room} już istnieje w tej rezerwacji.");

            var createReservationRoomResult = ReservationRoom
                .CreateReservationRoom(this, room, bookingAmount, childrenAmount, isForNewlyweds);

            if (createReservationRoomResult.IsError)
                return Result.Fail(createReservationRoomResult.Message);

            ReservationRooms.Add(createReservationRoomResult.Value);
            return Result.Ok();
        }

        public Result DeleteReservationRoom(ReservationRoom reservationRoom)
        {
            if (!ReservationRooms.Contains(reservationRoom))
                return Result.Fail($"{reservationRoom} nie należy do tej rezerwacji.");

            ReservationRooms.Remove(reservationRoom);
            return Result.Ok();
        }
    }
}
