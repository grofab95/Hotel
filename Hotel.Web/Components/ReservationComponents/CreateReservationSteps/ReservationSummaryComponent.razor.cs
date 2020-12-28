using Hotel.Application.Dtos;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Microsoft.AspNetCore.Components;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class ReservationSummaryComponent
    {
        [Parameter] public Reservation Reservation { get; set; }
        [Parameter] public PriceCalculator PriceCalculator { get; set; }
        [Parameter] public ReservationFactors ReservationFactors { get; set; }
    }
}
