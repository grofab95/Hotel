using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class PriceRuleDao : BaseDao, IPriceRuleDao
    {
        public PriceRuleDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<PriceRule> UpdateAsync(PriceRule priceRule)
        {
            await UpdateEntry(priceRule);
            return priceRule;
        }
      
        public async Task<PriceCalculator> GetPriceCalculator()
        {
            var priceRules = await GetManyAsync(x => x.Id > 0);

            return new PriceCalculator(priceRules);
        }

        public async Task<List<PriceRule>> GetManyAsync(Expression<Func<PriceRule, bool>> predicate)
        {
            return await context.PriceRules.Where(predicate).OrderBy(x => x.Priority).ToListAsync();
        }

        public Task<PriceRule> AddAsync(PriceRule entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PriceRule> GetAsync(Expression<Func<PriceRule, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
