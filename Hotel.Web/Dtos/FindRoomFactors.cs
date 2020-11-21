using System;

namespace Hotel.Web.Dtos
{
    public class FindRoomFactors
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int FindingAmount { get; set; }
    }
}
