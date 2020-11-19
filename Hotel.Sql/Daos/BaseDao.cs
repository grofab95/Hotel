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

        protected async Task UpdateEntry<T>(T entry)
        {
            AttachEntry(entry);

            await context.SaveChangesAsync();
        }

        protected void AttachEntry<T>(T entry)
        {
            if (context.Entry(entry).State != EntityState.Detached)
                return;

            context.Entry(entry).State = EntityState.Modified;
        }

        protected void AttachEntries<T>(List<T> entries) where T : Entity
        {
            var groupped = entries
                .GroupBy(x => x.Id)
                .Select(x => x.FirstOrDefault(y => context.Entry(y).State == EntityState.Detached))
                .ToList();

            groupped.ForEach(x => AttachEntry(x));
        }

        protected async Task<int> AddEntry<T>(T entry) where T : Entity
        {
            await context.AddAsync(entry);
            await context.SaveChangesAsync();

            return entry.Id;
        }
    }
}
