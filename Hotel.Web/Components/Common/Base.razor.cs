using Hotel.Web.Helpers;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Common
{
    public partial class Base
    {
        [Inject] NotificationService notificationService { get; set; }

        [Parameter] public dynamic Component { get; set; }

        public async Task DoSafeAction(Action action, string onWellMessage, string title = "Informacja")
        {
            try
            {
                action();

                await ShowNotification(new NotificationMessage
                {
                    Summary = title,
                    Duration = 6000,
                    Detail = onWellMessage
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

                //await Component.RefreshState();
                return true;
            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle());
            }

            return false;
        }

        public async Task<T> DoSaveFunc<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                await ShowNotification(ex.Handle());
            }

            return default(T);
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
    }
}
