using Hotel.Api.Middleware;
using Hotel.Api.Registrators;
using Hotel.Common;
using Hotel.Domain.Environment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hotel.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.RegisterLogger();
        services.RegisterControlers();
        services.RegisterApiBehaviorOptions();
        services.RegisterApiVersioning();
        services.RegisterContextFactory();
        services.RegisterAutoMapper();
        services.RegisterSwagger();
        services.RegisterAuthentication();
        services.RegisterDaos();           
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
    {
        app.UseMiddleware<ExceptionMiddleware>(logger);
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Api Documentation v1"));
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}