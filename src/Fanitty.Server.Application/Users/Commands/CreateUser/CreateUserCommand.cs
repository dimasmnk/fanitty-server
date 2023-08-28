using MediatR;

namespace Fanitty.Server.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest
{
    public string Uid { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
