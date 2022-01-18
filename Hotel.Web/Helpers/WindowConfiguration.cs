using Microsoft.AspNetCore.Components;
using Radzen;

namespace Hotel.Web.Helpers;

public class WindowConfiguration
{
    public string Title { get; set; }
    public RenderFragment Content { get; set; }
    public DialogOptions DialogOptions { get; set; }
}