using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class CreateReservationComponent
    {
        [Inject] IRoomDao RoomDao { get; set; }

        private SearchRoomDto _searchRoomDto;
        private Reservation _reservation;
        private List<Room> _rooms;

        protected override void OnInitialized()
        {
            _searchRoomDto = new SearchRoomDto();
        }

        private async Task SearchRooms(SearchRoomDto searchRoomDto)
        {
            try
            {
                _rooms = await RoomDao.GetFreeByDateRangeAsync(searchRoomDto.PeopleAmount,
                    new DateRange(searchRoomDto.CheckIn, searchRoomDto.CheckOut));

                _reservation = Reservation.Create(new Customer("Marta", "Nowak"), searchRoomDto.CheckIn, searchRoomDto.CheckOut);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
