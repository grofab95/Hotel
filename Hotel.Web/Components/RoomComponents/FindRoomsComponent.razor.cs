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

        private List<Room> _findedRooms;
        private FindRoomFactors _searchRoomDto;
        private bool _isRoomSearching;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _searchRoomDto = new FindRoomFactors
                {
                    CheckIn = DateTime.Now.AddDays(1),
                    CheckOut = DateTime.Now.AddDays(7),
                    FindingAmount = 6
                };
            }
            catch (Exception ex)
            {

            }
        }

        private async Task SearchRooms(FindRoomFactors findRoomFactors)
        {            
            await _base.DoSafeAction(async () =>
            {
                _isRoomSearching = true;

                var findedRooms = await RoomDao.GetFreeByDateRangeAsync(findRoomFactors.FindingAmount,
                    new DateRange(findRoomFactors.CheckIn, findRoomFactors.CheckOut));

                _isRoomSearching = false;

                await OnFindedRooms.InvokeAsync(new FindedRoomsFactors(findRoomFactors, findedRooms));
            });
        }
    }
}
