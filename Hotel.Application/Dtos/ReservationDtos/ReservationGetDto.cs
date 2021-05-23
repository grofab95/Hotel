using Hotel.Application.Dtos.CustomerDtos;
using Hotel.Application.Dtos.ReservationRoomDtos;
using System;
using System.Collections.Generic;

namespace Hotel.Application.Dtos.ReservationDtos
{
    public class ReservationGetDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
        public CustomerGetDto Customer { get; private set; }
        public List<ReservationRoomGetDto> ReservationRooms { get; private set; }
        public decimal TotalPrice { get; private set; }
    }
}
