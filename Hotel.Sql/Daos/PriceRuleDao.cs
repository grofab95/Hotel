using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql.ContextFactory;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hotel.Sql.Daos
{
    public class PriceRuleDao : BaseDao<PriceRule>, IPriceRuleDao
    {
        public PriceRuleDao(IContextFactory<HotelContext> contextFactory) : base(contextFactory)
        { }
              
        public async Task<PriceCalculator> GetPriceCalculator()
        {
            var priceRules = await context.PriceRules.ToListAsync();
            return new PriceCalculator(priceRules);
        }
    }
}
