using System.ComponentModel;

namespace Hotel.Domain.Entities.PriceRuleEntity;

public enum RuleType
{
    [Description("Zwiększaj o wartość")]
    IncreasingByValue,

    [Description("Zwiększaj o wartość procentową")]
    IncreasingByPercentage,

    [Description("Zmniejszaj o wartość")]
    DecreasingByValue,

    [Description("Zmniejszaj o wartość procentową")]
    DecreasingByPercentage
}