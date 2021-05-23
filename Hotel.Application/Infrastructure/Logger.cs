using Hotel.Domain.Environment;

namespace Hotel.Application.Infrastructure
{
    public class Logger : ILogger
    {
        private readonly Serilog.ILogger _serilog;

        public Logger()
        {
            _serilog = LoggerFactory.BuildSeriLog();
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
