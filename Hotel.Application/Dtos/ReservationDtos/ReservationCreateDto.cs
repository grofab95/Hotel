using Hotel.Application.Dtos.ReservationRoomDtos;
using System;
using System.Collections.Generic;

namespace Hotel.Application.Dtos.ReservationDtos
{
    public class ReservationCreateDto
    {
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
        public int CustomerId { get; private set; }
        //public List<ReservationRoomCreateDto> ReservationRooms { get; private set; }
    }
}
