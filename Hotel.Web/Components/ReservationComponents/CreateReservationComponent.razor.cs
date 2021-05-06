using Hotel.Application.Dtos.RoomDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class CreateReservationComponent
    {
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }
        [Inject] IReservationDao ReservationDao { get; set; }

        private Reservation _reservation;
        private PriceCalculator _priceCalculator;
        private ReservationFactors _reservationFactors;

        private List<Room> _findedRooms;   

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var priceRules = await PriceRuleDao.GetManyAsync(x => x.Id > 0);

                _priceCalculator = new PriceCalculator(priceRules);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }       
        }

        private async Task OnFindedRooms(FindedRoomsFactors findedRoomsFactors)
        {
            _reservationFactors = findedRoomsFactors.ReservationFactors;
            _findedRooms = findedRoomsFactors.FindedRooms;

            await DoSafeAction(() =>
            {
                _reservation = Reservation.Create(
                    customer: _reservationFactors.Customer,
                    checkIn: findedRoomsFactors.ReservationFactors.CheckIn,
                    checkOut: findedRoomsFactors.ReservationFactors.CheckOut);
            });
        }

        private async Task SaveReservation()
        {
            if (_reservation.GetGuestsAmount() < _reservationFactors.BookingAmount)
            {
                var isConfirmed = await ShowConfirm("Nie wszyscy goście zostali przydzieleni do pokoi, czy chcesz kontynuować?");
                if (!isConfirmed)
                    return;
            }

            try
            {
                ShowWaitingWindow("Trwa tworzenie rezerwacji ...");
                
                var reservationId = (await ReservationDao.AddAsync(_reservation)).Id;

                CloseWindow();

                Navigator.NavigateTo("reservations");

                await ShowNotification(new NotificationMessage
                {
                    Summary = "Informacja",
                    Duration = 6000,
                    Style = "width: auto;",
                    Severity = NotificationSeverity.Success,
                    Detail = $"Rezerwacja id {reservationId} została utworzona."
                });
            }
            catch (Exception ex)
            {
                await HandleException(ex);

                CloseWindow();
            }           
        }

        private async Task OnInitialDataChanged(ReservationFactors reservationFactors)
        {
            if (_reservation == null)
                return;

            var isSuccess = await DoSafeAction(() => 
            {
                _reservationFactors = reservationFactors;
                _reservation.ChangeCheckIn(reservationFactors.CheckIn);
                _reservation.ChangeCheckOut(reservationFactors.CheckOut);
                OnEvent(true);
            });

            reservationFactors.CheckIn = _reservation.CheckIn;
            reservationFactors.CheckOut = _reservation.CheckOut;

            StateHasChanged();
        }

        private void OnEvent(bool state) => StateHasChanged();
    }
}
