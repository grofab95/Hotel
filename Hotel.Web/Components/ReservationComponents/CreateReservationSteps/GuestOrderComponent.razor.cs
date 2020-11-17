using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class GuestOrderComponent
    {
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public Dictionary<int, List<GuestDto>> RoomIdGuests { get; set; }

        private void AddGuest(List<GuestDto> guestDtos)
        {
            guestDtos.Add(new GuestDto($"Gość {RoomIdGuests.SelectMany(x => x.Value).Count() + 1}"));
        }

        private void RemoveGuest(int roomId, GuestDto guestDto)
        {
            RoomIdGuests[roomId].Remove(guestDto);
        }
    }
}
