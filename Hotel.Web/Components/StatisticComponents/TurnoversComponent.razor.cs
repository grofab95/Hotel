using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.StatisticEntities;
using Hotel.Web.Helpers.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.StatisticComponents
{
    public partial class TurnoversComponent
    {
        [Inject] IStatisticDao statisticDao { get; set; }

        private TurnoverTypes _turnoverType;
        private Dictionary<int, string> _months;
        private List<int> _years;
        private Turnover _turnover;

        private int _selectedMonth = 1;
        private int _selectedYear = 2020;
        private bool _isInitalDataLoaded = false;
        private bool _isTurnoverLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var firstReservationYear = await statisticDao.GetFirstReservationYearAsync();

                _years = new List<int>();
                for (int year = firstReservationYear; year <= DateTime.Now.Year; year++)
                {
                    _years.Add(year);
                }

                var currentCultureMonths = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
                _months = DateTimeFormatInfo.CurrentInfo.MonthNames
                    .Where(x => !string.IsNullOrEmpty(x))
                    .ToDictionary(x => currentCultureMonths.IndexOf(x) + 1, x => x);

                _isInitalDataLoaded = true;
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void OnInputChange()
        {
            _turnover = null;

            StateHasChanged();
        }

        private async Task ShowTurnover()
        {
            try
            {
                _isTurnoverLoaded = true;

                int? selectedMonth = _turnoverType == TurnoverTypes.Monthly ? _selectedMonth : null;
                _turnover = await statisticDao.GetTurnoverAsync(_selectedYear, selectedMonth);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }

            _isTurnoverLoaded = false;
        }
    }
}
