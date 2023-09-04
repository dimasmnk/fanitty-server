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
        var userId = currentUserService.UserId.HasValue
            ? currentUserService.UserId.ToString()
            : currentUserService.Uid;

        if (elapsedMilliseconds < 500)
        {
            _logger.LogInformation("[Request: {Name}] [Duration: {ElapsedMilliseconds}ms] [User: {@UserId}] [StatusCode: {@StatusCode}]\r\n",
                path, elapsedMilliseconds, userId ?? "None", code);
        }
        else
        {
            _logger.LogWarning("[Request: {Name}] [Duration: {ElapsedMilliseconds}ms] [User: {@UserId}] [StatusCode: {@StatusCode}]\r\n",
                path, elapsedMilliseconds, userId ?? "None", code);
        }

        _timer.Reset();
    }
}
