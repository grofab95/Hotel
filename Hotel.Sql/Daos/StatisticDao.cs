using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.StatisticEntities;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class StatisticDao : BaseDao, IStatisticDao
    {
        public StatisticDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<int> GetFirstReservationYearAsync()
        {
            var firstReservation = await context.Reservations.OrderBy(x => x.CheckIn).FirstOrDefaultAsync();

            return firstReservation != null
                ? firstReservation.CheckIn.Year
                : DateTime.Now.Year;
        }

        public async Task<Turnover> GetTurnoverAsync(int year, int? month)
        {
            var monthsDays = 0;
            if (month != null)
                monthsDays = DateTime.DaysInMonth(year, (int)month);

            Expression<Func<Reservation, bool>> filter = 
                (x => x.CheckIn >= new DateTime(year, month ?? 1, 1, 0, 0, 0) && 
                      x.CheckIn <= new DateTime(year, month ?? 12, month != null ? monthsDays : 31, 23, 59, 59));

            var reservationsIds = await context.Reservations
                .Where(filter)
                .Select(x => x.Id)
                .ToListAsync();

            var guests = await context.Guests
                .Where(x => reservationsIds.Contains(x.ReservationRoom.Reservation.Id))
                .ToListAsync();

            return new Turnover
            {
                Income = guests.Sum(x => x.PriceForStay),
                PeopleAmount = guests.Count(),
                BreakfestAmount = guests.Where(x => x.OrderedBreakfest).Count()
            };
        }
    }
}
