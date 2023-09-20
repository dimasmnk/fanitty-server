using Fanitty.Server.API.Endpoints;
using Fanitty.Server.API.Extensions.Authentication;
using Fanitty.Server.API.Extensions.Cors;
using Fanitty.Server.API.Extensions.Logger;
using Fanitty.Server.API.Middlewares;
using Fanitty.Server.API.Services;
using Fanitty.Server.Application;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.AddFirebaseAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAppCors();
builder.AddLogger();

var app = builder.Build();

app.UserAppCors();
app.UseMiddleware<UnhandledExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapUsernameEndpoints();

app.Run();
