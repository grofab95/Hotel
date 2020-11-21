using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Entities.Views;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task GetReservation(int id)
        {
            _selectedReservation = null;
            StateHasChanged();

            _selectedReservation = await _base.DoSaveFunc(() => ReservationDao.GetReservationByIdAsync(id))?.Result;
        }

        private async Task SaveChanges()
        { 

            await _base.DoSafeAction(() =>
            {
                _selectedReservation.GetCalculatedPrice(_priceCalculator);
                ReservationDao.SaveReservationAsync(_selectedReservation);
            }, "Zmiany zostały zapisane.");
        }

        private void OnFindedRooms(FindedRoomsFactors findedRoomsFactors)
        {
            _findedRooms = findedRoomsFactors.FindedRooms;

            _base.CloseWindow();

            StateHasChanged();
        }
    }
}
