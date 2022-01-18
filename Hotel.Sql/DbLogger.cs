using Microsoft.Extensions.Logging;

namespace Hotel.Sql;

public class DbLogger
{
    public static readonly ILoggerFactory Factory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });
}