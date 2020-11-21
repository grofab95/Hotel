using Hotel.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Common
{
    public partial class Base
    {
        [Inject] NotificationService notificationService { get; set; }

        [Parameter] public dynamic Component { get; set; }

        public async Task DoSafeAction(Action action, string onWellMessage, string title = "Informacja",
            NotificationSeverity severity = NotificationSeverity.Success)
        {
            try
            {
                //await Task.Run(() => action());

                //await DoSafeFunc<Task>(() => { action(); return default; });

                await ShowNotification(new NotificationMessage
                {
                    Summary = title,
                    Duration = 6000,
                    Detail = onWellMessage,
                    Severity = severity
                });
            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle());
            }
        }

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

        public async Task<T> DoSafeFunc<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle());
            }

            return default;
        }

        public async Task<T> DoSafeFunc<T>(Func<Task<T>> func, string onWellMessage, string title = "Informacja",
            NotificationSeverity severity = NotificationSeverity.Success)
        {
            try
            {
                var result = await func();

                await ShowNotification(new NotificationMessage
                {
                    Summary = title,
                    Duration = 6000,
                    Detail = onWellMessage,
                    Severity = severity
                });

                return result;

            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle());
            }

            return default;
        }

        public async Task DoSafeActions(List<Action> actions)
        {
            foreach (var action in actions)           
                await DoSafeAction(action);            
        }

        public async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);
            await InvokeAsync(() => { StateHasChanged(); });
        }

        public async Task ShowWindow(WindowConfiguration windowConfiguration)
        {
            await window.Show(windowConfiguration);
        }

        public void CloseWindow() => window.Close();
    }
}
