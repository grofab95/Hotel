using Hotel.Application.Dtos.RoomDtos;
using Hotel.Application.Services;
using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Entities.Views;
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
        private ReservationFactors _reservationFactors;
        private List<Room> _findedRooms;

        private bool _roomSelectionActive = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _priceCalculator = await PriceRuleDao.GetPriceCalculator();
                _reservations = await ReservationDao.GetReservationBasicInfosAsync();
                _findedRooms = new List<Room>();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task LoadReservation(int id)
        {
            try
            {
                _selectedReservation = null;
                StateHasChanged();

                _selectedReservation = await ReservationDao.GetAsync(x => x.Id == id);
                _reservationFactors = new ReservationFactors
                {
                    BookingAmount = _selectedReservation.GetGuestsAmount(),
                    CheckIn = _selectedReservation.CheckIn,
                    CheckOut = _selectedReservation.CheckOut
                };

                _findedRooms.AddRange(_selectedReservation.GetRooms());
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task DeleteReservation(int reservationId)
        {
            if (!(await ShowConfirm($"Czy napewno chcesz usunać rezerwację o id {reservationId} ?")))
                return;

            try
            {
                await ReservationDao.DeleteAsync(reservationId);

                _reservations.Remove(_reservations.First(x => x.ReservationId == reservationId));
                StateHasChanged();

                await ShowNotification("Rezerwacja została usunięta.", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private async Task SaveChanges()
        {
            try
            {
                _selectedReservation.GetReservationPrice(_priceCalculator);

                await ReservationDao.UpdateAsync(_selectedReservation);

                await ShowNotification("Zmiany został zapisane", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);

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
            //_reservationFactors = findedRoomsFactors.ReservationFactors;

            _roomSelectionActive = true;
            CloseWindow();

            StateHasChanged();
        }

        private void OnEvent(bool state) => StateHasChanged();

        private async Task GetDocument(int reservationId)
        {
            try
            {
                ShowWaitingWindow("Trwa generowanie ...");

                var reservation = await ReservationDao.GetAsync(x => x.Id == reservationId);
                var reservationDocumentService = new ReservationDocumentService(reservation);
                var document = reservationDocumentService.GetCreatedDocumentName();
                var url = $"documents\\{document}.docx";

                CloseWindow();

                await JsRuntime.InvokeAsync<object>("open", new string[] { url, "_blank" });
            }
            catch (Exception ex)
            {
                await HandleException(ex);

                CloseWindow();
            }
        }
    }
}
