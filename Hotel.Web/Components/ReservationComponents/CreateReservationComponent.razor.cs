using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Web.Dtos;
using Hotel.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class CreateReservationComponent
    {
        [Inject] IRoomDao RoomDao { get; set; }  
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }
        [Inject] IReservationDao ReservationDao { get; set; }

        private SearchRoomDto _searchRoomDto;        
        private List<Room> _rooms;
        private Reservation _reservation;
        private PriceCalculator _priceCalculator;

        private bool _isRoomSearching;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var priceRules = await PriceRuleDao.GetAllAsync();
                _priceCalculator = new PriceCalculator(priceRules);

                _searchRoomDto = new SearchRoomDto
                {
                    CheckIn = DateTime.Now.AddDays(1),
                    CheckOut = DateTime.Now.AddDays(7),
                    BookingAmount = 6
                };
            }
            catch (Exception ex)
            {

            }       
        }

        private async Task SearchRooms(SearchRoomDto searchRoomDto)
        {
            await _base.DoSafeAction(async () =>  
            {
                _reservation = Reservation.Create(
                  customer: new Customer("Marta", "Nowak"),
                  checkIn: searchRoomDto.CheckIn,
                  checkOut: searchRoomDto.CheckOut);

                _isRoomSearching = true;

                _rooms = await RoomDao.GetFreeByDateRangeAsync(searchRoomDto.BookingAmount,
                    new DateRange(searchRoomDto.CheckIn, searchRoomDto.CheckOut));
                _isRoomSearching = false;

                OnEvent();
            });     
        }

        private async Task CreateReservation()
        {
            var reservationId = await _base.DoSaveFunc(() => ReservationDao.CreateReservationAsync(_reservation)).Result;
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
