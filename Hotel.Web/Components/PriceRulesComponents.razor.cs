using Hotel.Application.Dtos.PriceRuleDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Extensions;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components
{
    public partial class PriceRulesComponents
    {
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }

        private PriceRuleManager _priceRuleManager;
        private RadzenGrid<PriceRuleGetDto> _grid;

        private List<PriceRule> _priceRules;
        private List<PriceRuleGetDto> _priceRulesDtos;
        private Dictionary<RuleType, string> _ruleTypesNames;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var priceRules = await PriceRuleDao.GetManyAsync(x => x.Id > 0);
                _priceRuleManager = new PriceRuleManager(priceRules);
                _priceRules = _priceRuleManager.GetOrderedRules();

                _ruleTypesNames = Enum.GetValues(typeof(RuleType)).Cast<RuleType>()
                    .ToDictionary(x => x, x => x.GetDescription());

                _priceRulesDtos = Mapper.Map<List<PriceRuleGetDto>>(_priceRules);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void EditRow(PriceRuleGetDto priceRule)
        {
            _grid.EditRow(priceRule);
        }

        private async Task SaveRow(PriceRuleGetDto priceRuleDto)
        {
            try
            {
                var priceRule = _priceRules.Find(x => x.Id == priceRuleDto.Id);

                var updated = priceRule.Update(Mapper.Map<PriceRule>(priceRuleDto));
                await PriceRuleDao.UpdateAsync(updated);

                await _grid.UpdateRow(priceRuleDto);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void CancelEdit(PriceRuleGetDto priceRule)
        {
            _grid.CancelEditRow(priceRule);
        }
    }
}
