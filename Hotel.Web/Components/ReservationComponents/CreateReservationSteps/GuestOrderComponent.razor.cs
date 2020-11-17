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
        [Parameter] public ReservationFactors ReservationFactors { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }

        private async Task AddGuest(List<GuestDto> guestDtos)
        {
            var standardPriceForStay = Config.Get.PriceForStay;

            guestDtos.Add(new GuestDto($"Gość", standardPriceForStay));
            await OnEvent.InvokeAsync(true);
        }

        private async Task RemoveGuest(int roomId, GuestDto guestDto)
        {
            ReservationFactors.RoomIdGuests[roomId].Remove(guestDto);
            await OnEvent.InvokeAsync(true);
        }

        private async Task PriceChanged(decimal price, GuestDto guestDto)
        {
            if (price < 0)
                guestDto.PriceForStay = 0;

            if (price > 999)
                guestDto.PriceForStay = 999;

            await OnEvent.InvokeAsync(true);
        }
    }
}
