using Hotel.Domain.Entities;
using Hotel.Web.Components.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class RoomsSelectionComponent : Base
    {
        [Parameter] public List<Room> Rooms { get; set; }
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }

        private List<Room> _rooms;
        private bool _isLoaded;

        //protected override void OnInitialized()
        //{
        //    _rooms = new List<Room>();

        //    SetRooms();
        //}

        private void SetRooms()
        {
            _isLoaded = false;

            var rooms = new List<Room>();

            if (Rooms != null)
                rooms.AddRange(Rooms);

            if (Reservation != null)
            {
                var roomsInReservation = Reservation.GetRooms();
                //roomsInReservation.ForEach(x => x.SetNote("W rezerwacji"));

                rooms.AddRange(roomsInReservation);
            }

            _rooms = rooms.GroupBy(x => x.Id).Select(x => x.First()).ToList();

            _isLoaded = true;
        }

        protected override void OnParametersSet()
        {
            SetRooms();
        }

        private async Task RoomCheckedHandler(bool isChecked, Room room)
        {
            await DoSafeAction(Reservation.IsRoomInReservation(room)
                ? () => Reservation.DeleteRoom(room)
                : () => Reservation.AddRoom(room));     

            await OnEvent.InvokeAsync(true);
        }
    }
}
