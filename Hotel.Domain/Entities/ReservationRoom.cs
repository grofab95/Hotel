using Hotel.Domain.Entities.Common;
using Hotel.Domain.Excetions;
using Hotel.Domain.Utilities;
using System;

namespace Hotel.Domain.Entities
{
    public class ReservationRoom : Entity
    {
        public Room Room { get; private set; }
        public int BookingAmount { get; private set; }
        public int ChildrenAmount { get; private set; }
        public bool IsForNewlyweds { get; private set; }
        public Reservation Reservation { get; private set; }

        private ReservationRoom() { }

        internal static Result<ReservationRoom> CreateReservationRoom(Reservation reservation, Room room, 
            int bookingAmount, int childrenAmount, bool isForNewlyweds)
        {
            try
            {
                if (reservation == null)
                    throw new HotelException($"Rezerwacja jest wymagana.");

                if (room == null)
                    throw new HotelException("Pokój jest wymagany.");

                if (bookingAmount <= 0)
                    throw new HotelException("Zarezerwowana ilość w pokoju musi być większa od zera.");

                if (bookingAmount > room.PeopleAmount)
                    throw new HotelException("Ilość osób w rezerwacji pokoju nie może większa niż możliwa ilość osób w pokoju.");
            }
            catch (Exception ex)
            {
                return Result<ReservationRoom>.Fail(ex.Message);
            }

            return Result<ReservationRoom>.Ok(new ReservationRoom 
            {
                Room = room,
                Reservation = reservation,
                BookingAmount = bookingAmount,
                ChildrenAmount = childrenAmount,
                IsForNewlyweds = isForNewlyweds
            });
        }
    }
}
