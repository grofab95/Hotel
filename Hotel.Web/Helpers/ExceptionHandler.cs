using Radzen;
using System;

namespace Hotel.Web.Helpers
{
    public static class ExceptionHandler
    {
        public static NotificationMessage Handle(this Exception ex)
        {
            if (ex.GetType() == typeof(ApplicationException))
            {
                return new NotificationMessage
                {
                    Detail = ex.Message,
                    Duration = 6000,
                    Severity = NotificationSeverity.Error,
                    Summary = "Błąd"
                };
            }

            //Logger.Log(ex, true);

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
