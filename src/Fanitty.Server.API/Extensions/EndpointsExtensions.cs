using Fanitty.Server.API.Endpoints;

namespace Fanitty.Server.API.Extensions;

public static class EndpointsExtensions
{
    public static void MapAllEndpoints(this WebApplication app)
    {
        app.MapUserAuthEndpoints();
        app.MapUserEndpoints();
        app.MapUsernameEndpoints();
    }
}
