using Fanitty.Server.API.Extensions;
using Fanitty.Server.API.Extensions.Authentication;
using Fanitty.Server.API.Extensions.Authorization;
using Fanitty.Server.API.Extensions.Cors;
using Fanitty.Server.API.Extensions.HealthChecks;
using Fanitty.Server.API.Extensions.Logger;
using Fanitty.Server.API.Middlewares;
using Fanitty.Server.API.Services;
using Fanitty.Server.Application;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure;
using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

        using var scope = app.Services.CreateScope();
        using var db = scope.ServiceProvider.GetService<FanittyDbContext>()!;
        db.Database.Migrate();

        app.UserAppCors();
        app.UseMiddleware<UnhandledExceptionMiddleware>();
        app.UseMiddleware<LoggingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAllEndpoints();
        app.MapHealthChecks("/health");

        app.Run();
    }
}