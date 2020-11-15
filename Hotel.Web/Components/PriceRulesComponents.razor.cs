using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql;
using Hotel.Web.Dtos;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components
{
    public partial class PriceRulesComponents
    {
        private PriceRuleManager _priceRuleManager;
        private HotelContext _context;
        private RadzenGrid<PriceRuleDto> _grid;

        private List<PriceRule> _priceRules;
        private List<PriceRuleDto> _priceRulesDtos;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _context = new HotelContext();
                var priceRules = await _context.PriceRules.ToListAsync();
                _priceRuleManager = new PriceRuleManager(priceRules);
                _priceRules = _priceRuleManager.GetOrderedRules();
                _priceRulesDtos = _priceRules.Select(x => new PriceRuleDto(x)).ToList();
            }
            catch (Exception ex)
            {

            }
        }

        private void EditRow(PriceRuleDto priceRule)
        {
            _grid.EditRow(priceRule);
        }

        private void SaveRow(PriceRuleDto priceRule)
        {
            //var updatedPriceRule = new PriceRule();
        }

        private void CancelEdit(PriceRuleDto priceRule)
        {
            _grid.CancelEditRow(priceRule);
        }
    }
}
