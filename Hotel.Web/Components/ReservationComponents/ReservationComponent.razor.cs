using Hotel.Application.Dtos;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Web.Components.ReservationComponents
{
    public partial class ReservationComponent
    {
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public PriceCalculator PriceCalculator { get; set; }
        [Parameter] public ReservationFactors ReservationFactors { get; set; }
        [Parameter] public List<Room> Rooms { get; set; }
        [Parameter] public bool ActiveRoomSelection { get; set; }
        [Parameter] public EventCallback<bool> OnEvent { get; set; }

        private async Task CallEvent() => await OnEvent.InvokeAsync();
    }
}
