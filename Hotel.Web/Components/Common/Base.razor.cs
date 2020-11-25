using Hotel.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Common
{
    public partial class Base : ComponentBase
    {
        [Inject] NotificationService notificationService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }

        [Parameter] public dynamic Component { get; set; }


        public async Task<bool> DoSafeAction(Action action)
        {
            try
            {
                action();

                return true;
            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle());
            }

            return false;
        }

        protected async Task HandleException(Exception exception) => await ShowNotification(exception.Handle());

        protected async Task ShowNotification(string detail, NotificationSeverity severity, string summary = "Info", double duration = 6000)
            => await ShowNotification(new NotificationMessage 
            {
                Summary = summary,
                Severity = severity,
                Detail = detail,
                Duration = duration
            });

        public async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ShowWindow(WindowConfiguration windowConfiguration)
        {
            await window.Show(windowConfiguration);
        }

        public NavigationManager Navigator => NavigationManager;

        public void CloseWindow() => window.Close();

        public async Task<bool> ShowConfirm(string message) => await JsRuntime.InvokeAsync<bool>("confirm", message);
        public void ShowWaitingWindow(string waitingText) => window.ShowWaiting(waitingText);
    }
}
