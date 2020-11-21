using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Web.Dtos;
using Hotel.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class CreateReservationComponent
    {
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }
        [Inject] IReservationDao ReservationDao { get; set; }

        private Reservation _reservation;
        private PriceCalculator _priceCalculator;

        private List<Room> _findedRooms;

        private int _findedAmount;
        private bool _isRoomSearching;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var priceRules = await PriceRuleDao.GetAllAsync();

                _priceCalculator = new PriceCalculator(priceRules);
            }
            catch (Exception ex)
            {

            }       
        }

        private async Task OnFindedRooms(FindedRoomsFactors findedRoomsFactors)
        {
            _findedAmount = findedRoomsFactors.FindRoomFactors.FindingAmount;
            _findedRooms = findedRoomsFactors.FindedRooms;

            await _base.DoSafeAction(() =>
            {
                _reservation = Reservation.Create(
                    customer: new Customer("Ewa", "Blazor"),
                    checkIn: findedRoomsFactors.FindRoomFactors.CheckIn,
                    checkOut: findedRoomsFactors.FindRoomFactors.CheckOut);
            });
        }

        private async Task SaveReservation()
        {
            var reservationId = await _base.DoSafeFunc(
                () => ReservationDao.SaveReservationAsync(_reservation));

            if (reservationId == default)
                return;

            await _base.ShowNotification(new NotificationMessage
            {
                Summary = "Informacja",
                Duration = 6000,
                Style = "width: auto;",
                Severity = NotificationSeverity.Success,
                Detail = $"Rezerwacja id {reservationId} została utworzona."
            });
        }

        private void OnEvent() => StateHasChanged();
    }
}
