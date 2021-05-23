using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.Views;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class StatisticDao : IStatisticDao
    {
        private HotelContext _context;

        public StatisticDao(IContextFactory<HotelContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public async Task<List<ReservationInfoView>> GetAllAsync()
        {
            return await _context.ReservationInfoViews.ToListAsync();
        }
    }
}
