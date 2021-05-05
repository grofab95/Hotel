namespace Hotel.Domain.Environment
{
    public interface ILogger
    {
        void Log(string message, LogLevel logLevel);
    }
}
