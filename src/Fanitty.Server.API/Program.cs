using Fanitty.Server.API.Endpoints;
using Fanitty.Server.API.Extensions.Authentication;
using Fanitty.Server.API.Extensions.Cors;
using Fanitty.Server.API.Services;
using Fanitty.Server.Application;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.AddFirebaseAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAppCors();

var app = builder.Build();

app.UserAppCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();

app.Run();
