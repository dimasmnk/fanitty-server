using Fanitty.Server.Application.Queries.Usernames;
using MediatR;

namespace Fanitty.Server.API.Endpoints;

public static class UsernameEndpoints
{
    public static void MapUsernameEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("/usernames");

        userEndpoints.MapGet("/check/{username}",
            async (string username, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(new CheckUsernameAvailabilityQuery(username), cancellationToken));
    }
}
