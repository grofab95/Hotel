using Hotel.Domain.Adapters;
using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities;
using Hotel.Sql.ContextFactory;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class AreaDao : BaseDao, IDao
    {
        public AreaDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        {
        }

        public Task<Area> AddAsync(Area entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Area> GetAsync(Expression<Func<Area, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Area>> GetManyAsync(Expression<Func<Area, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Area> UpdateAsync(Area entity)
        {
            throw new NotImplementedException();
        }
    }
}
