using Fanitty.Server.Application.Responses.Users;
using MediatR;

namespace Fanitty.Server.Application.Queries.Users;
public class GetCurrentUserQuery : IRequest<CurrentUserResponse>
{
}
