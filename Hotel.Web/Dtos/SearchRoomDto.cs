using System;

namespace Hotel.Web.Dtos
{
    public class SearchRoomDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int PeopleAmount { get; set; }
    }
}
