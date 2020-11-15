//using Blazored.LocalStorage;
//using Core.InventorySystem.Common;
//using Core.InventorySystem.Domain.Adapters;
//using Core.InventorySystem.Domain.Dto.AuthDto;
//using Core.InventorySystem.Domain.Enums;
//using InventorySystem.Web.Dto;
//using InventorySystem.Web.Providers;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Authorization;
//using Radzen;
//using System.Threading.Tasks;

//namespace InventorySystem.Web.Components.Auth
//{
//    public partial class LoginComponent
//    {
//        [Inject] AuthenticationStateProvider authenticationStateProvider { get; set; }
//        [Inject] NotificationService notificationService { get; set; }
//        [Inject] NavigationManager navigationManager { get; set; }
//        [Inject] IUserDao userDao { get; set; }
//        [Inject] IAccountDao accountDao { get; set; }
//        [Inject] ILocalStorageService localStorage { get; set; }

//        private LoginDto _user;

//        private bool _isSigning;
//        private bool _anyUser = true;

//        protected override void OnInitialized()
//        {
//            _user = new LoginDto();
//        }

//        private async Task Login(LoginDto loginDto)
//        {
//            try
//            {
//                _isSigning = true;

//                var result = await accountDao.Authenticate(new AuthenticateRequest
//                {
//                    Email = loginDto.Email,
//                    Password = loginDto.Password,
//                    LaunchId = Program.LaunchId
//                },
//                null);

//                if (result == null)
//                {
//                    _anyUser = false;
//                    StateHasChanged();

//                    return;
//                }

//                await localStorage.SetItemAsync(TokenType.AccessToken.ToString(), result.AccessToken);
//                await localStorage.SetItemAsync(TokenType.RefreshToken.ToString(), result.RefreshToken);

//                await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

//                await ShowNotification(new NotificationMessage
//                {
//                    Detail = $"Zalogowano pomyślnie",
//                    Duration = 3000,
//                    Severity = NotificationSeverity.Success,
//                    Summary = "Informacja"
//                });
//            }
//            catch (System.ApplicationException ex)
//            {
//                await ShowNotification(new NotificationMessage
//                {
//                    Detail = ex.Message,
//                    Duration = 6000,
//                    Severity = NotificationSeverity.Error
//                });

//                _isSigning = false;
//            }
//            catch (System.Exception ex)
//            {
//                _isSigning = false;
//                var error = ex.Message;

//                if (ex.Message.Contains("No connection could be made because the target machine actively") ||
//                    ex.Message.Contains("Nie można nawiązać połączenia, ponieważ komputer docelowy aktywnie go odmawia"))
//                    error = "Brak dostępu do serwera.";

//                await ShowNotification(new NotificationMessage
//                {
//                    Detail = error,
//                    Duration = 4000,
//                    Severity = NotificationSeverity.Error
//                });

//                Logger.Log(ex, true);
//            }            
//        }
        
//        private async Task ShowNotification(NotificationMessage message)
//        {
//            notificationService.Notify(message);
//            await InvokeAsync(() => { StateHasChanged(); });
//        }
//    }
//}
