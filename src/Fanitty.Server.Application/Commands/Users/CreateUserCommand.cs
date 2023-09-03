using MediatR;

namespace Fanitty.Server.Application.Commands.Users;

public class CreateUserCommand : IRequest
{
    public string Username { get; set; } = string.Empty;
    public bool IsGeneratedUsername { get; set; }
}
