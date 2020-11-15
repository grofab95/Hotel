using Hotel.Domain.Entities.Common;
using Hotel.Domain.Excetions;
using System;

namespace Hotel.Domain.Entities.PriceRuleEntity
{
    public class PriceRule : Entity
    {
        public RuleName RuleName { get; private set; }        
        public RuleType RuleType { get; private set; }
        public string FriendlyName { get; private set; }
        public int Value { get; private set; }
        public int Priority { get; private set; }
        public bool ApplyNextRules { get; private set; }

        protected PriceRule() { }

        public PriceRule(RuleName ruleName, RuleType ruleType, string frienldyName, int value, int priority, bool applyNextRules)
        {
            RuleName = ruleName;
            RuleType = ruleType;

            if (string.IsNullOrEmpty(frienldyName) || string.IsNullOrWhiteSpace(frienldyName))
                throw new HotelException("Nazwa reguły jesy wymagana.");

            if (ruleType.ToString().Contains("Percentage"))
                if (value < 0 || value > 100)
                    throw new HotelException("Nieprawidłowa wartość procentowa reguły.");

            if (priority <= 0)
                throw new HotelException("Priorytet musi być większy od zera.");

            Value = value;
            Priority = priority;
            FriendlyName = frienldyName;
            ApplyNextRules = applyNextRules;
        }

        public decimal GetCalculatedPrice(decimal price) 
        {
            return RuleType switch
            {
                RuleType.IncreasingByValue => IncreaseValue(price, Value),
                RuleType.IncreasingByPercentage => IncreaseValue(price, GetValue(price)),
                RuleType.DecreasingByValue => DecreaseValue(price, Value),
                RuleType.DecreasingByPercentage => DecreaseValue(price, GetValue(price)),

                _ => throw new NotImplementedException(),
            };
        }

        private decimal GetValue(decimal price) => price * (Value * 0.01m);  
        private decimal IncreaseValue(decimal price, decimal value) => price + value;
        private decimal DecreaseValue(decimal price, decimal value) => price - value;

        public bool IsRuleObligatoring(RoomGuest roomGuest)
        {
            return RuleName switch
            {
                RuleName.PriceWhenBreakfest => roomGuest.OrderedBreakfest,
                RuleName.PriceWhenChild => roomGuest.IsChild,
                RuleName.PriceWhenNewlywed => roomGuest.IsNewlyweds,

                _ => throw new NotImplementedException()
            };
        }
    }
}
