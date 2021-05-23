using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class AreaDao : BaseDao<Area>, IAreaDao
    {
        public AreaDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        {
        }

        public override async Task DeleteAsync(int id)
        {
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new HotelException("Obszar nie został odnaleziony.");

            if (await context.Rooms.AnyAsync(x => x.Area.Id == id))
                throw new HotelException($"Pokoje są przydzielone do obszaru {area.Name}");

            context.Remove(area);
            await context.SaveChangesAsync();
        }

        public override async Task<Area> GetAsync(Expression<Func<Area, bool>> predicate)
        {
            return await context.Areas.FirstOrDefaultAsync(predicate)
                ?? throw new HotelException("Obszar nie został odnaleziony.");
        }

        public override async Task<Area> UpdateAsync(Area entity)
        {
            if (!(await context.Areas.AnyAsync(x => x.Id == entity.Id)))
                throw new HotelException($"Obszar {entity.Name} nie istnieje.");

            await UpdateEntry(entity);
            return entity;
        }
    }
}
