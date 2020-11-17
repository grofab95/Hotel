using Hotel.Domain.Entities;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class GuestOrderComponent
    {
        [Parameter] public Reservation Reservation { get; set; }
        private Dictionary<int, List<GuestDto>> _roomIdGuests;

        protected override void OnParametersSet()
        {
            _roomIdGuests = Reservation.ReservationRooms.ToDictionary(x => x.Room.Id, x => new List<GuestDto>());
        }

        private void AddGuest(List<GuestDto> guestDtos)
        {
            guestDtos.Add(new GuestDto
            {
                Name = "Gość 1"
            });
        }
    }
}
