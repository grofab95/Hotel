using AutoMapper;
using Blazored.LocalStorage;
using Hotel.Application.Infrastructure;
using Hotel.Common;
using Hotel.Domain.Environment;
using Hotel.Sql;
using Hotel.Sql.ContextFactory;
using Hotel.Web.Components.Interface;
using Hotel.Web.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using System.Reflection;

namespace Hotel.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<ILogger, Logger>();
            services.AddContextFactory<HotelContext>();
            services.AddBlazoredLocalStorage();
            services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>();
            services.AddAutoMapper(typeof(Hotel.Common.DomainMapperProfile).GetTypeInfo().Assembly);
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<NotificationService>();
            services.AddScoped<DialogService>();
            services.AddScoped<Window>();
            services.AddDaos();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
