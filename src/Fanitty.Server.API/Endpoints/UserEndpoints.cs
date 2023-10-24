using Fanitty.Server.API.Extensions.Authorization;
using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Queries.Users;
using MediatR;

namespace Fanitty.Server.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("/users").RequireAuthorization(AuthConstants.CreatedUserPolicy);

        userEndpoints.MapPut("/",
            async (UpdateUserCommand command, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(command, cancellationToken));

        userEndpoints.MapGet("/me",
            async (IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(new GetCurrentUserQuery(), cancellationToken));

        userEndpoints.MapGet("/{username}",
            async (string username, IMediator mediator, CancellationToken cancellationToken)
            => await mediator.Send(new GetUserByUsernameQuery(username), cancellationToken));
    }
}
