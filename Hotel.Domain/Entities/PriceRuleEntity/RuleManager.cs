using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities.PriceRuleEntity
{
    public class RuleManager
    {
        private List<PriceRule> _priceRules;

        public RuleManager(List<PriceRule> priceRules)
        {
            _priceRules = priceRules;
        }

        public PriceRule GetRuleByName(RuleName ruleName)  => _priceRules.FirstOrDefault(x => x.RuleName == ruleName);

        public List<PriceRule> GetOrderedRules() => _priceRules.OrderBy(x => x.Priority).ToList();

        public decimal GetCalculatedPrice(RuleName ruleName, decimal inputPrice)
        {
            var priceRule = GetRuleByName(ruleName);
            var calculatedPrice = priceRule.GetCalculatedPrice(inputPrice);

            return calculatedPrice;
        }
    }
}