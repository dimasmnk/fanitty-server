using Fanitty.Server.API.Endpoints;
using Fanitty.Server.API.Extensions.Authentication;
using Fanitty.Server.API.Extensions.Cors;
using Fanitty.Server.API.Logging;
using Fanitty.Server.API.Middlewares;
using Fanitty.Server.API.Services;
using Fanitty.Server.Application;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.AddFirebaseAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAppCors();
builder.Services.AddSingleton<ConsoleFormatter, CustomLogFormatter>();
builder.Logging.AddConsole(options =>
{
    options.FormatterName = "CustomLogFormatter";
});

var app = builder.Build();

app.UserAppCors();
app.UseMiddleware<UnhandledExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();

app.Run();
