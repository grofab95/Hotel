using AutoMapper;
using Hotel.Domain.Adapters;
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
        [Inject] public IMapper Mapper { get; set; }

        protected async Task<bool> DoSafeAction(Action action)
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

        protected async Task ShowNotification(string detail, NotificationSeverity severity, string summary = "Informacja", double duration = 6000)
            => await ShowNotification(new NotificationMessage 
            {
                Summary = summary,
                Severity = severity,
                Detail = detail,
                Duration = duration
            });

        protected async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ShowWindow(WindowConfiguration windowConfiguration)
        {
            await window.Show(windowConfiguration);
        }

        protected NavigationManager Navigator => NavigationManager;

        protected void CloseWindow() => window.Close();

        protected async Task<bool> ShowConfirm(string message) => await JsRuntime.InvokeAsync<bool>("confirm", message);
        protected void ShowWaitingWindow(string waitingText) => window.ShowWaiting(waitingText);
    }
}
