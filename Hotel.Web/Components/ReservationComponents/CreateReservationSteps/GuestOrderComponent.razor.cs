using AutoMapper;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
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
        [Parameter] public PriceCalculator PriceCalculator { get; set; }
        [Parameter] public ReservationFactors ReservationFactors { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }
        [Inject] IMapper Mapper { get; set; }

        private decimal _standardPriceForStay;
        private Dictionary<Guest, bool> _guestStandardPrice;

        protected override void OnParametersSet()
        {
            _standardPriceForStay = Config.Get.PriceForStay;         
            _guestStandardPrice = Reservation.ReservationRooms.
                SelectMany(x => x.Guests)
                .ToDictionary(x => x, x => x.BasePrice == _standardPriceForStay);
        }

        private async Task AddGuest(ReservationRoom reservationRoom)
        {
            var isDone = await DoSafeAction(
                () => Reservation.AddGuestToRoom(reservationRoom, "Gość", false, false, false, _standardPriceForStay));

            if (!isDone)
                return;

            var addedGuest = reservationRoom.Guests.Last();
            _guestStandardPrice.Add(addedGuest, true);

            await CallEvent();
        }

        private async Task RemoveGuest(ReservationRoom reservationRoom, Guest guest)
        {
            var isDone = await DoSafeAction(() => Reservation.RemoveGuestFromRoom(reservationRoom, guest));
            if (!isDone)
                return;

            _guestStandardPrice.Remove(guest);

            await CallEvent();
        }

        private async Task PriceChanged(decimal price, Guest guest, GuestDto guestDto)
        {
            if (price < 0) price = 0;
            if (price > 999) price = 999;

            await UpdateGuest(guest, guestDto);
        }

        private async Task BasePriceStateChanged(bool state, Guest guest, GuestDto guestDto)
        {
            if (!state)
                return;

            guestDto.BasePrice = _standardPriceForStay;

            await UpdateGuest(guest, guestDto);
        }

        private async Task UpdateGuest(Guest guest, GuestDto guestDto)
        {
            await DoSafeAction(() => guest.Update(Mapper.Map<Guest>(guestDto)));

            await CallEvent();
        }
        
        private async Task CallEvent(bool state = false) => await OnEvent.InvokeAsync(true);
    }
}
