using System.Collections.Generic;

namespace Hotel.Application.Dtos.ReservationRoomDtos
{
    public class ReservationRoomCreateDto
    {
        public int RoomId { get; set; }
        public List<int> GuestIds { get; set; }
    }
}
