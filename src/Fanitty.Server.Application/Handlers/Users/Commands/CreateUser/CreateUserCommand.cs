using MediatR;

namespace Fanitty.Server.Application.Handlers.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest
{
    public string Username { get; set; } = string.Empty;
}
