using Hotel.Domain.Entities;
using System.Collections.Generic;

namespace Hotel.Application.Dtos
{
    public class FindedRoomsFactors
    {
        public ReservationFactors ReservationFactors { get; set; }
        public List<Room> FindedRooms { get; set; }

        public FindedRoomsFactors(ReservationFactors reservationFactors, List<Room> findedRooms)
        {
            ReservationFactors = reservationFactors;
            FindedRooms = findedRooms;
        }
    }
}
