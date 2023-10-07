using MediatR;

namespace Fanitty.Server.Application.Commands.Users;
public class UpdateUserCommand : IRequest
{
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }
}
