using Hotel.Web.Dtos;
using Microsoft.AspNetCore.Components;

namespace Hotel.Web.Components.ReservationComponents.CreateReservationSteps
{
    public partial class ReservationSummaryComponent
    {
        [Parameter] public ReservationFactors ReservationFactors { get; set; }
    }
}
