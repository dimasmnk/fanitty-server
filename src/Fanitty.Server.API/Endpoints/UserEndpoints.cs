using Fanitty.Server.Application.Handlers.Users.Commands.CreateUser;
using MediatR;

namespace Fanitty.Server.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("/users").RequireAuthorization();

        userEndpoints.MapPost("/",
            async (CreateUserCommand command, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(command, cancellationToken));
    }
}
