using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class RoomsSelectionComponent
    {
        [Parameter] public List<Room> Rooms { get; set; }
        [Parameter] public Reservation Reservation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        private void RoomCheckedHandler(bool state, Room room)
        {
            if (state)
            {
                Reservation.AddRoom(room);
                return;
            }

            Reservation.DeleteRoom(room);
        }
    }
}
