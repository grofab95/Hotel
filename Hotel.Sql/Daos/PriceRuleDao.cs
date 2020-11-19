using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class PriceRuleDao : BaseDao, IPriceRuleDao
    {
        public PriceRuleDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }

        public async Task<List<PriceRule>> GetAllAsync() => await context.PriceRules.OrderBy(x => x.Priority).ToListAsync();

        public async Task UpdateAsync(PriceRule priceRule) => await UpdateEntry(priceRule);
      
        public async Task<PriceCalculator> GetPriceCalculator()
        {
            var priceRules = await GetAllAsync();

            return new PriceCalculator(priceRules);
        }
    }
}
