using Hotel.Domain.Entities.PriceRuleEntity;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Application
{
    public class RuleManager
    {
        private List<PriceRule> _priceRules;

        public RuleManager(List<PriceRule> priceRules)
        {
            _priceRules = priceRules;
        }

        public PriceRule GetRuleByName(RuleName ruleName)
            => _priceRules.FirstOrDefault(x => x.RuleName == ruleName);
    }
}
