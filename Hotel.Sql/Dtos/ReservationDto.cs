using Hotel.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace Hotel.Sql.Dtos
{
    internal class ReservationDto : Entity
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public CustomerDto Customer { get; set; }
        public List<ReservationRoomDto> ReservationRooms { get; set; }
    }
}
