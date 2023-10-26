using Fanitty.Server.API.Extensions;
using Fanitty.Server.API.Middlewares;
using Fanitty.Server.API.Services;
using Fanitty.Server.Application;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure;
using Serilog;

namespace Fanitty.Server.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
        builder.Services.AddHttpContextAccessor();
        builder.AddFirebaseAuthentication();
        builder.AddConfiguredAuthorization();
        builder.AddHealthChecks();
        builder.Services.AddAppCors();
        builder.Services.AddSerilog();
        builder.AddLogger();

        var app = builder.Build();

        app.ApplyMigrations();

        app.UserAppCors();
        app.UseMiddleware<UnhandledExceptionMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");
        app.MapAllEndpoints();

        app.Run();
    }
}