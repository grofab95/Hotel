using Hotel.Domain.Entities.Common;
using System;

namespace Hotel.Domain.Entities.PriceRuleEntity
{
    public class PriceRule : Entity
    {
        public RuleName RuleName { get; private set; }        
        public RuleType RuleType { get; set; }
        public int PercetageValue { get; private set; }
        public int NumericValue { get; private set; }

        private PriceRule() { }

        public decimal GetCalculatedPrice(decimal price)
        {
            if (PercetageValue > 0)
                switch (RuleType)
                {
                    case RuleType.Increasing:
                        return price + (price * PercetageValue * 0.01m);

                    case RuleType.Decreasing:
                        return price - (price * PercetageValue * 0.01m);
                }

            if (NumericValue > 0)
                switch (RuleType)
                {
                    case RuleType.Increasing:
                        return price + NumericValue;

                    case RuleType.Decreasing:
                        return price - NumericValue;
                }

            throw new NotImplementedException();
        }
    }
}
