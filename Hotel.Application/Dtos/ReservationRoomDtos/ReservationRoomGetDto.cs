using Hotel.Application.Dtos.GuestDtos;
using Hotel.Application.Dtos.RoomDtos;
using System.Collections.Generic;

namespace Hotel.Application.Dtos.ReservationRoomDtos
{
    public class ReservationRoomGetDto
    {
        public RoomGetDto Room { get; private set; }
        public List<GuestGetDto> Guests { get; private set; }
    }
}
