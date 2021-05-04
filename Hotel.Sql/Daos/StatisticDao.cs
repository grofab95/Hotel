using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.StatisticEntities;
using Hotel.Domain.Entities.Views;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class StatisticDao : BaseDao, IStatisticDao
    {
        public StatisticDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<List<ReservationInfoView>> GetAllAsync()
        {
            return await context.ReservationInfoViews.ToListAsync();
        }

        public async Task<int> GetFirstReservationYearAsync()
        {
            var firstReservation = await context.Reservations.OrderBy(x => x.CheckIn).FirstOrDefaultAsync();

            return firstReservation != null
                ? firstReservation.CheckIn.Year
                : DateTime.Now.Year;
        }

        public async Task<Dictionary<int, List<int>>> GetReservationsMonthsAsync()
        {
            var reservations = await context.Reservations
                .Select(x => x.CheckIn)
                //.Distinct()
                .ToListAsync();

            var groupped = reservations
                .Select(x => new DateTime(x.Year, x.Month, 1))
                .Distinct()
                .GroupBy(x => x.Year)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Month).Distinct().OrderBy(y => y).ToList());

            return groupped;
        }

        public async Task<Turnover> GetTurnoverAsync(int year, int? month)
        {
            var monthsDays = 0;
            if (month != null)
                monthsDays = DateTime.DaysInMonth(year, (int)month);

            Func<Reservation, bool> request =
                (x => x.CheckIn >= new DateTime(year, month ?? 1, 1, 0, 0, 0) &&
                      x.CheckIn <= new DateTime(year, month ?? 12, month != null ? monthsDays : 31, 23, 59, 59));

            var xx = context.Reservations
                //.Where(request)
                //.Select(x => x.Id)
                .ToList();

            var reservationsIds = xx.Where(request).Select(x => x.Id).ToList();

            var reserPrices = await context.Reservations
                .Where(x => reservationsIds.Contains(x.Id))
                .Select(x => x.TotalPrice)
                .ToListAsync();


            var guests = await context.Guests
                .Where(x => reservationsIds.Contains(x.ReservationRoom.Reservation.Id))
                .ToListAsync();

            return null;

            //return new Turnover
            //{
            //    Income = reserPrices.Sum(x => x), // guests.Sum(x => x.PriceForStay),
            //    ReservationAmount = reservationsIds.Distinct().Count(),
            //    PeopleAmount = guests.Count(),
            //    BreakfestAmount = guests.Where(x => x.OrderedBreakfest).Count()
            //};
        }
    }
}
