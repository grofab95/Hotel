using System;

namespace Hotel.Domain.Entities.Views
{
    public class ReservationInfoView
    {
        public int ReservationId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Customer { get; set; }
        public int BookingAmount { get; set; }
    }
}
