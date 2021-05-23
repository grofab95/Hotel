using Hotel.Domain.Adapters.Common;
using Hotel.Domain.Entities.PriceRuleEntity;
using System.Threading.Tasks;

namespace Hotel.Domain.Adapters
{
    public interface IPriceRuleDao : IAddDao<PriceRule>, IModifyDao<PriceRule>, IGetDao<PriceRule>
    {
        Task<PriceCalculator> GetPriceCalculator();
    }
}
