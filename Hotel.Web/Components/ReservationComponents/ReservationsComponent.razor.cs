using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Entities.Views;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class ReservationsComponent
    {
        [Inject] IReservationDao ReservationDao { get; set; }
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }

        private List<ReservationInfoView> _reservations;
        private PriceCalculator _priceCalculator;
        private Reservation _selectedReservation;
        private List<Room> _findedRooms;

        private bool _roomSelectionActive = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _priceCalculator = await PriceRuleDao.GetPriceCalculator();
                _reservations = await ReservationDao.GetReservationBasicInfosAsync();
            }
            catch (Exception ex)
            {

            }
        }

        private async Task LoadReservation(int id)
        {
            _selectedReservation = null;
            StateHasChanged();

            _selectedReservation = await _base.DoSafeFunc(() => ReservationDao.GetReservationByIdAsync(id));
        }

        private async Task SaveChanges()
        {
            var isUpdated = await _base.DoSafeFunc(() =>
            {
                _selectedReservation.GetCalculatedPrice(_priceCalculator);
                return ReservationDao.UpdateReservationAsync(_selectedReservation);
            }, "Zmiany zostały zapisane.");

            if (!isUpdated)
            {
                await LoadReservation(_selectedReservation.Id);
                StateHasChanged();
            }
        }

        private async Task CancelChanges()
        {
            if (_selectedReservation == null)
                return;

            await LoadReservation(_selectedReservation.Id);
        }

        private void OnFindedRooms(FindedRoomsFactors findedRoomsFactors)
        {
            _findedRooms = findedRoomsFactors.FindedRooms;
            _roomSelectionActive = true;
            _base.CloseWindow();

            StateHasChanged();
        }
    }
}
