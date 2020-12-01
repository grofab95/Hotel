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
    public class AreaDao : BaseDao, IAreaDao
    {
        public AreaDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<Area> AddAsync(Area entity)
        {
            if (await context.Areas.AnyAsync(x => x.Name.ToLower().Trim() == entity.Name.ToLower().Trim()))
                throw new HotelException($"Obszar o takiej nazwie już istnieje");

            await context.Areas.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var area = await context.Areas.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new HotelException("Obszar nie został odnaleziony.");

            if (await context.Rooms.AnyAsync(x => x.Area.Id == id))
                throw new HotelException($"Pokoje są przydzielone do obszaru {area.Name}");

            context.Remove(area);
            await context.SaveChangesAsync();
        }

        public async Task<Area> GetAsync(Expression<Func<Area, bool>> predicate)
        {
            return await context.Areas.FirstOrDefaultAsync(predicate)
                ?? throw new HotelException("Obszar nie został odnaleziony.");
        }

        public async Task<List<Area>> GetManyAsync(Expression<Func<Area, bool>> predicate)
        {
            return await context.Areas.Where(predicate).ToListAsync();
        }

        public async Task<Area> UpdateAsync(Area entity)
        {
            if (!(await context.Areas.AnyAsync(x => x.Id == entity.Id)))
                throw new HotelException($"Obszar {entity.Name} nie istnieje.");

            await UpdateEntry(entity);
            return entity;
        }
    }
}
