using Fanitty.Server.Application.Interfaces;
using System.Diagnostics;

namespace Fanitty.Server.API.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Stopwatch _timer;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _timer = new Stopwatch();
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService)
    {
        _timer.Start();

        await _next(context);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        var path = context.Request.Path;
        var code = context.Response.StatusCode;
        var ip = context.Connection.RemoteIpAddress;
        var method = context.Request.Method;
        var uid = currentUserService.Uid ?? "None";

        if (elapsedMilliseconds < 500)
        {
            _logger.LogInformation("Request {Method} for {Name} with code {StatusCode} from user {UserId} {Ip} with duration {ElapsedMilliseconds}ms",
                method, path, code, uid, ip, elapsedMilliseconds);
        }
        else
        {
            _logger.LogWarning("Request {Method} for {Name} with code {StatusCode} from user {UserId} {Ip} with duration {ElapsedMilliseconds}ms",
                method, path, code, uid, ip, elapsedMilliseconds);
        }

        _timer.Reset();
    }
}
