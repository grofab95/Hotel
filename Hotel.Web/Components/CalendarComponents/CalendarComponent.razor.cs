using Hotel.Application.Dtos.ReservationDtos;
using Hotel.Domain.Adapters;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Components.CalendarComponents
{
    public partial class CalendarComponent
    {
        [Inject] IReservationDao ReservationDao { get; set; }

        private List<ReservationGetDto> _reservationsDtos;
        private RadzenScheduler<ReservationGetDto> _scheduler;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var basicReservationsInfo = await ReservationDao.GetReservationBasicInfosAsync();

                _reservationsDtos = basicReservationsInfo.Select(x => new ReservationGetDto
                {
                    Id = x.ReservationId,
                    CheckIn = x.CheckIn,
                    CheckOut = x.CheckOut.AddHours(Config.Get.FreeRoomHour),
                    Text = $"Rezerwacja {x.ReservationId} - {x.Customer}"
                }).ToList();

                RefillColors();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        private void RefillColors()
        {
            //var colors = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().Where(x => !x.ToString().Contains("Light")).ToList();
            var colors = new List<KnownColor>
            {
                KnownColor.Blue,
                KnownColor.Green,
                KnownColor.Purple,
                KnownColor.Violet,
                KnownColor.Orange,
                KnownColor.Gray,
                KnownColor.Cyan
            };

            var random = new Random();

            _reservationsDtos.ForEach(x => x.Color = colors[random.Next(colors.Count)]);
        }

        private void OnSchedulerRender(SchedulerAppointmentRenderEventArgs<ReservationGetDto> args)
        {            
            args.Attributes["style"] = $"background: {args.Data.Color}";
        }
    }
}
