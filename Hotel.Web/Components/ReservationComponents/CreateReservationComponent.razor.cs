using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
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

        private SearchRoomDto _searchRoomDto;        
        private List<Room> _rooms;
        private ReservationFactors _reservationFactors;

        private bool _isRoomSearching;

        protected override void OnInitialized()
        {
            _searchRoomDto = new SearchRoomDto
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(7),
                BookingAmount = 4
            };
        }

        private async Task SearchRooms(SearchRoomDto searchRoomDto)
        {
            _isRoomSearching = true;

            try
            {
                _rooms = await RoomDao.GetFreeByDateRangeAsync(searchRoomDto.BookingAmount,
                    new DateRange(searchRoomDto.CheckIn, searchRoomDto.CheckOut));

               var reservation = Reservation.Create(
                   customer: new Customer("Marta", "Nowak"),
                   checkIn: searchRoomDto.CheckIn,
                   checkOut: searchRoomDto.CheckOut);

                _reservationFactors = new ReservationFactors(reservation, searchRoomDto.BookingAmount);
            }
            catch (Exception ex)
            {

            }

            _isRoomSearching = false;
        }

        private void OnEvent() => StateHasChanged();
    }
}
