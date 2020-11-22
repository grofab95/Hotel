using Hotel.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        protected async Task UpdateEntry<T>(T entry) where T : Entity
        {
            await context.SaveChangesAsync();
        }

        protected void AttachEntry<T>(T entry) where T : Entity
        {
            if (entry.Id == 0)
                return;
            context.Entry(entry).State = EntityState.Modified;
        }

        protected void AttachEntries<T>(List<T> entries) where T : Entity
        {
            var groupped = entries
                .Where(x => /*context.Entry(x).State == EntityState.Detached &&*/ x.Id != 0)
                .GroupBy(x => x.Id)
                .Select(x => x.FirstOrDefault(/*y => context.Entry(y).State == EntityState.Detached)*/))
                .ToList();

            groupped.ForEach(x => AttachEntry(x));
        }
    }
}
