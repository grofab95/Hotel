using Hotel.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hotel.Sql.ContextFactory
{
    public static class DbContextExtensions
    {
        public static void DetachLocal<T>(this DbContext context, T t, int? entryId)
            where T : Entity
        {
            var local = context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entryId));
            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
