using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class RoomsSelectionComponent
    {
        [Parameter] public List<Room> Rooms { get; set; }
        [Parameter] public ReservationFactors ReservationFactors { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }

        private async Task RoomCheckedHandler(bool isChecked, Room room)
        {
            if (isChecked)            
                ReservationFactors.AddRoom(room);
            else
                ReservationFactors.DeleteRoom(room);

            await OnEvent.InvokeAsync(true);
        }
    }
}
