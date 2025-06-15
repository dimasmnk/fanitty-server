using Fanitty.Server.Application.Responses.Users;
using MediatR;

namespace Fanitty.Server.Application.Queries.Users;
public class GetUserByUsernameQuery : IRequest<UserByUsernameResponse>
{
    public string Username { get; set; } = string.Empty;

    public GetUserByUsernameQuery(string username)
    {
        Username = username;
    }
}
