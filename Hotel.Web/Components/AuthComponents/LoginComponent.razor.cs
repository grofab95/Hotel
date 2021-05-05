using Hotel.Web.Helpers;
using Hotel.Web.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace Hotel.Web.Components.AuthComponents
{
    public partial class LoginComponent
    {
        [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }

        private LoginDto _user;

        public LoginComponent()
        {
            _user = new LoginDto();
        }

        private async Task Login(LoginDto loginDto)
        {
            try
            {
                await ((AuthenticationProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginDto);
                StateHasChanged();
                Navigator.NavigateTo("/");
                await ShowNotification("Zalogowano pomyślnie", Radzen.NotificationSeverity.Success);
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }
    }
}
