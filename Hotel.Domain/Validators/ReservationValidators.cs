using Hotel.Domain.Entities;
using Hotel.Domain.Excetions;
using System.Linq;

namespace Hotel.Domain.Validators
{
    public class ReservationValidators
    {
        public static void ValidIfReservationRoomExistInReservation(Reservation reservation, ReservationRoom reservationRoom)
        {
            if (!reservation.ReservationRooms?.Any(x => x.Id == reservationRoom.Id) ?? true)
                throw new HotelException($"W tej rezerwacji nie istnieje pokój rezerwacyjny o id {reservationRoom.Id}.");
        }
    }
}
