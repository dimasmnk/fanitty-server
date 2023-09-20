using Fanitty.Server.Application.Responses.Usernames;
using MediatR;

namespace Fanitty.Server.Application.Queries.Usernames;
public class CheckUsernameAvailabilityQuery : IRequest<CheckUsernameAvailabilityResponse>
{
    public string Username { get; set; }

    public CheckUsernameAvailabilityQuery(string username)
    {
        Username = username;
    }
}
