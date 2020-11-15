using Hotel.Domain.Entities.Common;
using Hotel.Domain.Entities.PriceRuleEntity;

namespace Hotel.Web.Dtos
{
    public class PriceRuleDto : Entity
    {
        public RuleName RuleName { get; set; }
        public RuleType RuleType { get; set; }
        public string FriendlyName { get; set; }
        public int Value { get; set; }
        public int Priority { get; set; }
        public bool ApplyNextRules { get; set; }

        public PriceRuleDto(PriceRule priceRule)
        {
            Id = priceRule.Id;
            CreatedAt = priceRule.CreatedAt;
            UpdatedAt = priceRule.UpdatedAt;
            RuleName = priceRule.RuleName;
            RuleType = priceRule.RuleType;
            FriendlyName = priceRule.FriendlyName;
            Value = priceRule.Value;
            Priority = priceRule.Priority;
            ApplyNextRules = priceRule.ApplyNextRules;
        }
    }
}
