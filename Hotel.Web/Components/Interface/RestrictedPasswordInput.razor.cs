//using Core.InventorySystem.Common;
//using Core.InventorySystem.Domain.Adapters;
//using InventorySystem.CustomUtilities;
//using InventorySystem.Web.Helpers.Enums;
//using Microsoft.AspNetCore.Components;
//using Radzen;
//using System;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;

//namespace Hotel.Web.Components.Interface
//{
//    public partial class RestrictedPasswordInput
//    {
//        [Parameter] public EventCallback<string> ButonClicked { get; set; }
//        [Parameter] public RestrictedPasswordInput SlaveRestrictedPasswordInput { get; set; }
//        [Parameter] public PasswordInputType PasswordInputType { get; set; }
//        [Parameter] public string Style { get; set; }

//        [Inject] NotificationService notificationService { get; set; }
//        [Inject] ICustomValueDao customValueDao { get; set; }

//        private PasswordRestrictionValidator _passwordRestrictionValidator;
//        private string _password;
//        private string _error;
//        private string _inputStyle;

//        protected override async Task OnInitializedAsync()
//        {
//            try
//            {
//                _inputStyle = Style;

//                if (PasswordInputType == PasswordInputType.Master)
//                {
//                    _passwordRestrictionValidator = await PasswordRestrictionValidator.CreateValidator(customValueDao);
//                    //if (SlaveRestrictedPasswordInput == null)
//                    //    throw new ApplicationException("Brak referencji do podrzędnego komponentu. ");
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log(ex, true);
//            }
//        }

//        public void ErrorHandler(bool isError)
//        {
//            _inputStyle = Style;

//            if (isError)
//                _inputStyle += "outline: 1px solid red;";

//            StateHasChanged();
//        }

//        public async Task<Result<string>> GetPassword()
//        {
//            try
//            {
//                var isValid = await Valid();
//                if (!isValid)
//                    return Result<string>.Fail("");

//                if (PasswordInputType == PasswordInputType.Master)
//                    SlaveRestrictedPasswordInput.ErrorHandler(false);

//                return Result<string>.Ok(value: _password);
//            }
//            catch (ArgumentException ex)
//            {
//                _inputStyle += "outline: 1px solid red;";
//                _error = ex.Message;
//                if (PasswordInputType == PasswordInputType.Master && SlaveRestrictedPasswordInput != null)
//                    SlaveRestrictedPasswordInput.ErrorHandler(true);

//                StateHasChanged();

//                return Result<string>.Fail("");
//            }
//            catch (FormatException ex)
//            {
//                _inputStyle += "outline: 1px solid red;";
//                StateHasChanged();

//                await ShowNotification(new NotificationMessage
//                {
//                    Detail = ex.Message,
//                    Duration = 10000,
//                    Severity = NotificationSeverity.Warning,
//                    Summary = "Wymagania hasła:"
//                });

//                return Result<string>.Fail("");
//            }
//            catch (Exception ex)
//            {
//                _inputStyle += "outline: 1px solid red;";
//                StateHasChanged();

//                await ShowNotification(new NotificationMessage
//                {
//                    Detail = ex.Message,
//                    Duration = 10000,
//                    Severity = NotificationSeverity.Error,
//                    Summary = "Błąd"
//                });

//                Logger.Log(ex, true);

//                return Result<string>.Fail("");
//            }
//        }

//        private async Task<bool> Valid()
//        {
//            _inputStyle = Style;
//            _error = null;

//            if (string.IsNullOrEmpty(_password))
//                throw new ArgumentException("Hasło jest wymagane.");

//            if (PasswordInputType == PasswordInputType.Master)
//            {
//                var verficationResult = _passwordRestrictionValidator.Verify(_password);
//                if (verficationResult.IsError)
//                    throw new FormatException(string.Join(", ", verficationResult.Value));

//                var slaveValueResult = await SlaveRestrictedPasswordInput.GetPassword();
//                if (slaveValueResult.IsError)
//                    return false;

//                if (slaveValueResult.Value.ToLower().Trim() != _password.ToLower().Trim())
//                    throw new ArgumentException("Hasła róźnią się.");
//            }

//            return true;
//        }

//        private async Task ShowNotification(NotificationMessage message)
//        {
//            notificationService.Notify(message);
//            await InvokeAsync(() => { StateHasChanged(); });
//        }
//    }
//}
