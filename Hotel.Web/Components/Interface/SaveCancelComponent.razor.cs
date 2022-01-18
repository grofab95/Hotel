using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Hotel.Web.Components.Interface;

public partial class SaveCancelComponent
{
    [Parameter] public EventCallback<bool> OnSave { get; set; }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }
    [Parameter] public string CancelText { get; set; }
    [Parameter] public bool SaveDisabled { get; set; } = false;
    [Parameter] public bool CancelDisabled { get; set; } = false;

    [Inject] IJSRuntime JsRuntime { get; set; }

    private string _cancelText;

    protected override void OnInitialized()
    {
        _cancelText = string.IsNullOrEmpty(CancelText)
            ? "Czy napewno chcesz anulować zmiany?"
            : CancelText;
    }

    private async Task OnSaveHandler()
    {
        await OnSave.InvokeAsync(true);
    }

    private async Task OnCancelHandler()
    {
        var confirmed = await JsRuntime.InvokeAsync<bool>("confirm", _cancelText);
        if (!confirmed)
            return;

        await OnCancel.InvokeAsync(true);
    }
}