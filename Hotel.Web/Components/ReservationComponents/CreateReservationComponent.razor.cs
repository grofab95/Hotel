using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class CreateReservationComponent
    {
        [Inject] IRoomDao RoomDao { get; set; }  
        [Inject] IPriceRuleDao PriceRuleDao { get; set; }

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
                    CheckIn = DateTime.Now,
                    CheckOut = DateTime.Now.AddDays(7),
                    BookingAmount = 4
                };
            }
            catch (Exception ex)
            {

            }            
        }

        private async Task SearchRooms(SearchRoomDto searchRoomDto)
        {
            _isRoomSearching = true;

            try
            {
                _rooms = await RoomDao.GetFreeByDateRangeAsync(searchRoomDto.BookingAmount,
                    new DateRange(searchRoomDto.CheckIn, searchRoomDto.CheckOut));

                _reservation = Reservation.Create(
                   customer: new Customer("Marta", "Nowak"),
                   checkIn: searchRoomDto.CheckIn,
                   checkOut: searchRoomDto.CheckOut);

                //_reservation = new ReservationFactors(reservation, searchRoomDto.BookingAmount);
            }
            catch (Exception ex)
            {

            }

            _isRoomSearching = false;
        }

        private void OnEvent() => StateHasChanged();
    }
}
