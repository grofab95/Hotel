using Hotel.Domain.Entities;
using System.Collections.Generic;

namespace Hotel.Web.Dtos
{
    public class FindedRoomsFactors
    {
        public FindRoomFactors FindRoomFactors { get; set; }
        public List<Room> FindedRooms { get; set; }

        public FindedRoomsFactors(FindRoomFactors findRoomDto, List<Room> findedRooms)
        {
            FindRoomFactors = findRoomDto;
            FindedRooms = findedRooms;
        }
    }
}
