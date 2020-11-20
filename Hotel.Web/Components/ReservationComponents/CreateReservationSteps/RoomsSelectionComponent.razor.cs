using Hotel.Domain.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class RoomsSelectionComponent
    {
        [Parameter] public List<Room> Rooms { get; set; }
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }

        protected override void OnInitialized()
        {
            if (Rooms == null && Reservation != null)
            {
                Rooms = Reservation.ReservationRooms.Select(x => x.Room).ToList();
                Rooms.ForEach(x => x.SetNote($"W rezerwacji"));
            }
        }

        private async Task RoomCheckedHandler(bool isChecked, Room room)
        {
            if (isChecked)            
                await _base.DoSafeAction(() => Reservation.AddRoom(room));
            else
                await _base.DoSafeAction(() => Reservation.DeleteRoom(room));

            await OnEvent.InvokeAsync(true);
        }
    }
}
