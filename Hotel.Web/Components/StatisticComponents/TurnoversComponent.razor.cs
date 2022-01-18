using Hotel.Domain.Adapters;
using Hotel.Domain.Entities.StatisticEntities;
using Hotel.Domain.Entities.Views;
using Hotel.Domain.Extensions;
using Hotel.Web.Helpers.Enums;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.StatisticComponents;

public partial class TurnoversComponent
{
    [Inject] IStatisticDao statisticDao { get; set; }

    private List<ReservationInfoView> _reservationsInfos;

    private TurnoverTypes _turnoverType;
    private Dictionary<int, string> _months;
    private Dictionary<int, List<int>> _reservationsMonths;
    private List<int> _years;
    private Turnover _turnover;

    private int _selectedMonth;
    private int _selectedYear;
    private bool _isInitalDataLoaded = false;
    private bool _isTurnoverLoaded = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _reservationsInfos = await statisticDao.GetAllAsync();
            _years = _reservationsInfos.Select(x => x.CheckIn.Year).OrderBy(x => x).Distinct().ToList();
            _selectedYear = DateTime.Now.Year;
            _selectedMonth = DateTime.Now.Month;
            _turnoverType = TurnoverTypes.Monthly;
            if (_reservationsInfos.Any())
                await OnInputChange();

            _isInitalDataLoaded = true;
        }
        catch (Exception ex)
        {
            await HandleException(ex);
        }
    }

    private async Task OnInputChange()
    {
        try
        {
            LoadMonths();
            GateDateRange(out DateTime startDate, out DateTime endDate);
            var reservations = _reservationsInfos
                .Where(x => x.CheckIn > startDate && x.CheckIn < endDate)
                .ToList();

            _turnover = new Turnover(reservations);
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await HandleException(ex);
        }
    }

    private void GateDateRange(out DateTime startDate, out DateTime endDate)
    {
        startDate = DateTime.MinValue;
        endDate = DateTime.MinValue;
        if (_turnoverType == TurnoverTypes.Yearly)
        {
            startDate = new DateTime(_selectedYear, 1, 1, 0, 0, 0);
            endDate = new DateTime(_selectedYear, 12, 31, 23, 59, 59);
            return;
        }
        var montDays = DateTime.DaysInMonth(_selectedYear, _selectedMonth);
        startDate = new DateTime(_selectedYear, _selectedMonth, 1, 0, 0, 0);
        endDate = new DateTime(_selectedYear, _selectedMonth, montDays, 23, 59, 59);
    }

    private void LoadMonths()
    {
        var currentCultureMonths = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
        var allMonths = DateTimeFormatInfo.CurrentInfo.MonthNames
            .Where(x => !string.IsNullOrEmpty(x))
            .ToDictionary(x => currentCultureMonths.IndexOf(x) + 1, x => x);

        var monthsWithReservations = _reservationsInfos
            .Where(x => x.CheckIn.Year == _selectedYear)
            .OrderBy(x => x.CheckIn.Month)
            .Select(x => x.CheckIn.Month)
            .Distinct()
            .ToList();

        _months = allMonths
            .GetSame(monthsWithReservations, x => x.Key, x => x)
            .ToDictionary(x => x.Key, x => x.Value);

        if (!monthsWithReservations.Contains(_selectedMonth))
            _selectedMonth = monthsWithReservations.FirstOrDefault();
    }
}