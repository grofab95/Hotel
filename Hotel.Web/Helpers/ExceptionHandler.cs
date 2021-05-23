using Hotel.Domain.Environment;
using Hotel.Domain.Exceptions;
using Radzen;
using System;

namespace Hotel.Web.Helpers
{
    internal static class ExceptionHandler
    {
        public static NotificationMessage Handle(this Exception ex, ILogger logger)
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

            logger.Log(ex.ToString(), LogLevel.Error);

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
