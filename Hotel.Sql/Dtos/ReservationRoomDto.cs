using Hotel.Domain.Entities.Common;
using System.Collections.Generic;

namespace Hotel.Sql.Dtos
{
    internal class ReservationRoomDto : Entity
    {
        public RoomDto Room { get; set; }
        public ReservationDto Reservation { get; set; }
        public List<GuestDto> Guests { get; set; }
    }
}
