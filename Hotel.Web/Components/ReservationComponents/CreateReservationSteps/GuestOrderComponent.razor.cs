using Hotel.Domain.Entities;
using Hotel.Domain.Environment;
using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class GuestOrderComponent
    {
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }

        private async Task AddGuest(ReservationRoom reservationRoom)
        {
            var standardPriceForStay = Config.Get.PriceForStay;

            Reservation.AddGuestToRoom(reservationRoom, "Gość", false, false, false);

            //guestDtos.Add(new GuestDto($"Gość", standardPriceForStay));
            await CallEvent();
        }

        private async Task RemoveGuest(ReservationRoom reservationRoom, Guest guest)
        {
            Reservation.RemoveGuestFromRoom(reservationRoom, guest);
            //ReservationFactors.RoomIdGuests[roomId].Remove(guestDto);
            await CallEvent();
        }

        private async Task PriceChanged(decimal? price, Guest guestDto)
        {
            if (price is null)
                return;

            if (price < 0)
                guestDto.PriceForStay = 0;

            if (price > 999)
                guestDto.PriceForStay = 999;

            await CallEvent();
        }

        private async Task CallEvent(bool state = false) => await OnEvent.InvokeAsync(true);
    }
}
