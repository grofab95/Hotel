using Hotel.Domain.Adapters;
using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.RoomComponents
{
    public partial class FindRoomsComponent
    {
        [Inject] IRoomDao RoomDao { get; set; }
        [Parameter] public EventCallback<FindedRoomsFactors> OnFindedRooms { get; set; }
        [Parameter] public EventCallback<ReservationFactors> OnChange { get; set; }

        private List<Room> _findedRooms;
        private ReservationFactors _reservationFactors;
        private bool _isRoomSearching;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _reservationFactors = new ReservationFactors
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

        private async Task DataChanged() => await OnChange.InvokeAsync(_reservationFactors);

        private async Task SearchRooms(ReservationFactors reservationFactors)
        {            
            await DoSafeAction(async () =>
            {
                _isRoomSearching = true;

                var findedRooms = await RoomDao.GetFreeByDateRangeAsync(reservationFactors.BookingAmount,
                    new DateRange(reservationFactors.CheckIn, reservationFactors.CheckOut));

                _isRoomSearching = false;

                await OnFindedRooms.InvokeAsync(new FindedRoomsFactors(reservationFactors, findedRooms));
            });
        }
    }
}
