using Fanitty.Server.Application.Commands.Users;
using MediatR;

namespace Fanitty.Server.API.Endpoints;

public static class UserAuthEndpoints
{
    public static void MapUserAuthEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("/users").RequireAuthorization();

        userEndpoints.MapPost("/",
            async (IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(new CreateUserCommand(), cancellationToken));
    }
}
