using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities.PriceRuleEntity;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IPriceRuleDao : IDao<PriceRule>
    {
        Task<PriceCalculator> GetPriceCalculator();
    }
}
