using Hotel.Domain.Exceptions;
using Radzen;
using Serilog;
using System;
using System.Linq;

namespace Hotel.Web.Helpers
{
    public static class ExceptionHandler
    {
        public static NotificationMessage Handle(this Exception ex)
        {
            if (ex.GetType() == typeof(HotelException) || ex.GetType() == typeof(MissingValueException))
            {
                return new NotificationMessage
                {
                    Detail = ex.Message,
                    Duration = 6000,
                    Severity = NotificationSeverity.Error,
                    Summary = "Błąd"
                };
            }

            Log.Error(ex.ToString());

            return new NotificationMessage
            {
                Detail = "Wystąpił nieoczekiwany błąd.",
                Duration = 6000,
                Severity = NotificationSeverity.Error,
                Summary = "Błąd"
            };
        }
    }
}
