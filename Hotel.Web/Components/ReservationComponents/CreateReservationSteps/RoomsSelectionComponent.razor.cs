using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class RoomsSelectionComponent
    {
        [Parameter] public List<Room> Rooms { get; set; }
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public Dictionary<int, List<GuestDto>> RoomIdGuests { get; set; }

        private void RoomCheckedHandler(bool state, Room room)
        {
            if (state)
            {
                Reservation.AddRoom(room);
                RoomIdGuests.Add(room.Id, new List<GuestDto> { /*new GuestDto($"Gość {RoomIdGuests.SelectMany(x => x.Value).Count() + 1}")*/ });
                return;
            }

            Reservation.DeleteRoom(room);

            RoomIdGuests.Remove(room.Id);
        }
    }
}
