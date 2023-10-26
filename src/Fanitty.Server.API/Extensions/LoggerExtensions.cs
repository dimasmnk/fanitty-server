using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Fanitty.Server.API.Extensions;

public static class LoggerExtensions
{
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) =>
        {
            lc.WriteTo.Console(theme: AnsiConsoleTheme.Literate);
            lc.ReadFrom.Configuration(ctx.Configuration);
        });

        return builder;
    }
}
