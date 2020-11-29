using Hotel.Domain.Entities;
using System;

namespace Hotel.Web.Dtos
{
    public class ReservationFactors
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int BookingAmount { get; set; }
        public Customer Customer { get; set; }
    }
}
