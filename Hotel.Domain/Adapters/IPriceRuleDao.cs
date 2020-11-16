using Hotel.Domain.Entities.PriceRuleEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IPriceRuleDao
    {
        Task<List<PriceRule>> GetAllAsync();
        Task UpdateAsync(PriceRule priceRule);
    }
}
