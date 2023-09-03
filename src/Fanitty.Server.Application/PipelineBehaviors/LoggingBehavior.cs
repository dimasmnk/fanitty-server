using Fanitty.Server.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Fanitty.Server.Application.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehavior(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId.HasValue
            ? _currentUserService.UserId.ToString()
            : _currentUserService.Uid;

        if (elapsedMilliseconds < 500)
        {
            _logger.LogInformation("[Request: {Name}] [Duration: {ElapsedMilliseconds}ms] [User: {@UserId}]\r\n",
                requestName, elapsedMilliseconds, userId ?? "None");
        }
        else
        {
            _logger.LogWarning("[Request: {Name}] [Duration: {ElapsedMilliseconds}ms] [User: {@UserId}]\r\n",
                requestName, elapsedMilliseconds, userId ?? "None");
        }

        return response;
    }
}
