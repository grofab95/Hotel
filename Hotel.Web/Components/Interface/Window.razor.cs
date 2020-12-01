using Hotel.Web.Helpers;
using Microsoft.JSInterop;
using Radzen;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Interface
{
    public partial class Window /*: IDisposable*/
    {
        private DialogService _dialogService;
        private IJSRuntime _jsRuntime;

        public Window(DialogService dialogService, IJSRuntime JsRuntime)
        {
            _dialogService = dialogService;
            _jsRuntime = JsRuntime;
        }

        public async Task Show(WindowConfiguration windowConfiguration)
        {
            ShowWindow(windowConfiguration);

            await Task.Delay(500);

            await _jsRuntime.InvokeVoidAsync("enableDraggableDialog");
        }

        public void ShowSync(WindowConfiguration windowConfiguration)
        {
            ShowWindow(windowConfiguration);
        }

        public void ShowWaiting(string waitingText)
        {
            ShowWaitingWindow(waitingText);
        }

        public void Close() => _dialogService.Close();

        public DialogService Service => _dialogService;

        public void Dispose()
        {
            _dialogService.Close();
        }
    }
}
