using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;

namespace Hotel.Web
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                   .Enrich.FromLogContext()
                   .WriteTo.Console()
                   .WriteTo.File(
                       "logs//LOG_.log",
                       outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                       //outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}",
                       restrictedToMinimumLevel: LogEventLevel.Information,
                       rollingInterval: RollingInterval.Day
                       )
                   .CreateLogger();

                Log.Error("Test loggera");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error($"Wystapil problem podczas uruchamiania serwera: {ex.InnerException?.Message ?? ex.Message}");

                return -1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return 1;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
