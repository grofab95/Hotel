using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities.Common;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public abstract class BaseDao<T> : IGetDao<T>, ICountDao<T>, IAddDao<T>, IModifyDao<T>
        where T : Entity
    {
        protected HotelContext context;

        public BaseDao(ContextFactory.IContextFactory<HotelContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected async Task<T> UpdateEntry(T entry) 
        {
            AttachEntry(entry);

            await context.SaveChangesAsync();

            return entry;
        }

        protected void AttachEntry<E>(E entry) where E : Entity
        {
            if (entry.Id == 0)
                return;

            context.Entry(entry).State = EntityState.Modified;
        }

        protected void AttachEntries<E>(List<E> entries) where E : Entity
        {
            var groupped = entries
                .Where(x => /*context.Entry(x).State == EntityState.Detached &&*/ x.Id != 0)
                .GroupBy(x => x.Id)
                .Select(x => x.FirstOrDefault(/*y => context.Entry(y).State == EntityState.Detached)*/))
                .ToList();

            groupped.ForEach(x => AttachEntry(x));
        }

        public virtual async Task<List<T>> GetManyAsync(int page, int limit, Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
               .Where(predicate)
               .OrderBy(x => x.Id)
               .Pagging(page, limit)
               .ToListAsync();
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).CountAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var deleteQuery = $"DELETE FROM [{typeof(T).Name}s] WHERE {nameof(Entity.Id)}={id}";
            var deletedAmount = await context.Database.ExecuteSqlRawAsync(deleteQuery);
            if (deletedAmount == 0)
                throw new HotelException("Usunięcie się nie powiodło.");
        }
    }
}
