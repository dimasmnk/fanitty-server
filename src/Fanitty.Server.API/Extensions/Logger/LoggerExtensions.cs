using Fanitty.Server.API.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Fanitty.Server.API.Extensions.Logger;

public static class LoggerExtensions
{
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ConsoleFormatter, CustomLogFormatter>();
        builder.Logging.AddConsole(options =>
        {
            options.FormatterName = "CustomLogFormatter";
        });

        return builder;
    }
}
