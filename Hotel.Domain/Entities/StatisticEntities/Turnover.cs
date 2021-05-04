using Hotel.Domain.Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities.StatisticEntities
{
    public class Turnover
    {
        public decimal Income { get; set; }
        public int PeopleAmount { get; set; }
        public int BreakfestAmount { get; set; }
        public int ReservationAmount { get; set; }

        public bool AnyInfo => Income > 0;

        public Turnover(List<ReservationInfoView> reservations)
        {
            Income = reservations.Sum(s => s.TotalPrice);
            PeopleAmount = reservations.Sum(x => x.BookingAmount);
            BreakfestAmount = reservations.Sum(x => x.BreakfestAmount);
            ReservationAmount = reservations.Count();
        }
    }
}
