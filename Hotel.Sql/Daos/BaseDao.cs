using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public abstract class BaseDao
    {
        protected HotelContext context;

        public BaseDao(ContextFactory.IContextFactory<HotelContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected async Task UpdateEntry<T>(T entry)
        {
            context.Entry(entry).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
    }
}
