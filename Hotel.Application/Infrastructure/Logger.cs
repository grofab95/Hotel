using Hotel.Domain.Environment;
using Serilog;
using Serilog.Events;

namespace Hotel.Application.Infrastructure
{
    public class Logger : Domain.Environment.ILogger
    {
        private readonly Serilog.ILogger _serilog;

        public Logger()
        {
            _serilog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    "logs//LOG_.log",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Log(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    _serilog.Information(message);
                    break;

                case LogLevel.Error:
                    _serilog.Error(message);
                    break;

                case LogLevel.Fatal:
                    _serilog.Fatal(message);
                    break;

                default:
                    break;
            }
        }
    }
}
