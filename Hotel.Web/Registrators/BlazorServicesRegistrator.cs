using Blazored.LocalStorage;
using Hotel.Web.Components.Interface;
using Hotel.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Radzen;

namespace Hotel.Web.Registrators
{
    public static class BlazorServicesRegistrator
    {
        public static void RegisterBlazorFactors(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();
        }

        public static void RegisterRadzenFactors(this IServiceCollection services)
        {
            services.AddScoped<NotificationService>();
            services.AddScoped<DialogService>();
            services.AddScoped<Window>();
        }

        public static void RegisterAuthentication(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>();
        }
    }
}
