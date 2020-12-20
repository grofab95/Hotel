using System;
using System.Drawing;

namespace Hotel.Web.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Text { get; set; }
        public KnownColor Color { get; set; }
    }
}
