using Hotel.Application.Dtos.GuestDtos;
using System.Collections.Generic;

namespace Hotel.Application.Dtos.ReservationRoomDtos
{
    public class ReservationRoomGetDto
    {
        public int RoomId { get; private set; }
        public List<GuestGetDto> Guests { get; private set; }
    }
}
