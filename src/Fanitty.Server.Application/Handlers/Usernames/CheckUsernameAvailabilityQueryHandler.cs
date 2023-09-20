using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Application.Queries.Usernames;
using Fanitty.Server.Application.Responses.Usernames;
using MediatR;

namespace Fanitty.Server.Application.Handlers.Usernames;
public class CheckUsernameAvailabilityQueryHandler : IRequestHandler<CheckUsernameAvailabilityQuery, CheckUsernameAvailabilityResponse>
{
    private IUserRepository _userRepository;

    public CheckUsernameAvailabilityQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CheckUsernameAvailabilityResponse> Handle(CheckUsernameAvailabilityQuery request, CancellationToken cancellationToken)
    {
        var isAvailable = await _userRepository.IsUsernameAvailable(request.Username, cancellationToken);
        var response = new CheckUsernameAvailabilityResponse
        {
            IsAvailable = isAvailable
        };

        return response;
    }
}
