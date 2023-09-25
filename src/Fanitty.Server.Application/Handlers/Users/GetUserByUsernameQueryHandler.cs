using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Application.Mappers;
using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Application.Responses.Users;
using MediatR;

namespace Fanitty.Server.Application.Handlers.Users;
public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserByUsernameResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByUsernameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserByUsernameResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken);
        return user.MapToUserByUsernameResponse();
    }
}
