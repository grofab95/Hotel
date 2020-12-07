using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using System.Linq;

namespace Hotel.Domain.Validators
{
    public class ReservationValidator
    {
        private Reservation _reservation;

        protected ReservationValidator(Reservation reservation)
        {
            _reservation = reservation;
        }

        public static void Check(Reservation reservation)
        {
            new ReservationValidator(reservation).Check();
        }

        protected void Check()
        {
            if (_reservation.GetRoomsAmount() == 0)
                throw new HotelException("Rezerwacja nie zawiera pokoi.");

            if (_reservation.GetGuestsAmount() == 0)
                throw new HotelException("Rezerwacja nie zawiera gości.");

            var guests = _reservation.GetGuests();
            if (!guests.All(x => x.BasePrice > 0)) // dodać wykluczenie tych co maja swoja cene
                throw new HotelException("Nie wszyscy goście mają ustawioną cenę bazową.");

            if (_reservation.TotalPrice == 0)
                throw new HotelException("Cena za całość nie została wyliczona.");
        }
    }
}
