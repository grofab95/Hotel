using AutoMapper;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Extensions;
using Hotel.Web.Dtos;
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
        [Inject] IMapper Mapper { get; set; }
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }

        private PriceRuleManager _priceRuleManager;
        private RadzenGrid<PriceRuleDto> _grid;

        private List<PriceRule> _priceRules;
        private List<PriceRuleDto> _priceRulesDtos;
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

                _priceRulesDtos = Mapper.Map<List<PriceRuleDto>>(_priceRules);
            }
            catch (Exception ex)
            {

            }
        }

        private void EditRow(PriceRuleDto priceRule)
        {
            _grid.EditRow(priceRule);
        }

        private async Task SaveRow(PriceRuleDto priceRuleDto)
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

            }

            //await DoSafeAction<bool>(async () => 
            //{
            //    var priceRule = _priceRules.Find(x => x.Id == priceRuleDto.Id);

            //    var updated = priceRule.Update(Mapper.Map<PriceRule>(priceRuleDto));
            //    await PriceRuleDao.UpdateAsync(updated);

            //    await _grid.UpdateRow(priceRuleDto);
            //}, "Zapisano zmiany");
        }

        private void CancelEdit(PriceRuleDto priceRule)
        {
            _grid.CancelEditRow(priceRule);
        }
    }
}
