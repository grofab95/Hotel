using AutoMapper;
using Hotel.Domain.Environment;
using Hotel.Web.Components.Interface;
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
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] Window Window { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Inject] public IMapper Mapper { get; set; }
        [Inject] public ILogger Logger { get; set; }

        protected async Task<bool> DoSafeAction(Action action)
        {
            try
            {
                action();

                return true;
            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle(Logger));
            }

            return false;
        }

        protected async Task HandleException(Exception exception) => await ShowNotification(exception.Handle(Logger));

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
            NotificationService.Notify(message);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ShowWindow(WindowConfiguration windowConfiguration)
        {
            await Window.Show(windowConfiguration);
        }

        protected async Task<bool> ShowConfirm(string message)
        {
            try
            {
                return await JsRuntime.InvokeAsync<bool>("confirm", message);
            }
            catch (Exception)
            { }

            return false;
        }

        protected NavigationManager Navigator => NavigationManager;

        protected void CloseWindow() => Window.Close();

        protected void ShowWaitingWindow(string waitingText) => Window.ShowWaiting(waitingText);
    }
}
