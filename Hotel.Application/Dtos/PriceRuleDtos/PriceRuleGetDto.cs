using Hotel.Domain.Entities.Common;
using Hotel.Domain.Entities.PriceRuleEntity;

namespace Hotel.Application.Dtos.PriceRuleDtos
{
    public class PriceRuleGetDto : Entity
    {
        public RuleName RuleName { get; set; }
        public RuleType RuleType { get; set; }
        public string FriendlyName { get; set; }
        public int Value { get; set; }
        public int Priority { get; set; }
        public bool ApplyNextRules { get; set; }
    }
}
