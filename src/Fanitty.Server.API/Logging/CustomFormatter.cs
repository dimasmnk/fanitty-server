using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Fanitty.Server.API.Logging;

public class CustomLogFormatter : ConsoleFormatter
{
    private const string defaultColor = "\x1B[0m";
    public CustomLogFormatter() : base(nameof(CustomLogFormatter))
    {
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var timestamp = DateTime.UtcNow;
        var logLevel = logEntry.LogLevel;
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

        var logLevelColor = GetAnsiColorCode(logLevel);

        if (logEntry.Exception is null)
        {
            textWriter.WriteLine($"[{timestamp}] [{logLevelColor}{logLevel}{defaultColor}]: {message}");
        }
        else
        {
            textWriter.WriteLine($"[{timestamp}] [{logLevelColor}{logLevel}{defaultColor}]: {logEntry.Exception.GetType().FullName} {logEntry.Exception.Message}");
            textWriter.WriteLine($"StackTrace: {logEntry.Exception.StackTrace}");
        }
    }

    private static string GetAnsiColorCode(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "\x1B[37m", // White
            LogLevel.Debug => "\x1B[37m", // White
            LogLevel.Information => "\x1B[37m", // Green
            LogLevel.Warning => "\x1B[33m", // Yellow
            LogLevel.Error => "\x1B[31m", // Red
            LogLevel.Critical => "\x1B[41m", // Red background
            _ => "\x1B[0m", // Reset
        };
    }
}
