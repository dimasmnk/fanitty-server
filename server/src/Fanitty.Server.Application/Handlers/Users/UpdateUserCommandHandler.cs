using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using MediatR;

namespace Fanitty.Server.Application.Handlers.Users;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IFanittyDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateUserCommandHandler(IFanittyDbContext dbContext, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (!string.IsNullOrEmpty(request.DisplayName))
            user.DisplayName = request.DisplayName;

        if (!string.IsNullOrEmpty(request.Username))
        {
            var isUsernameAvailable = await _userRepository.IsUsernameAvailableAsync(request.Username, cancellationToken);
            if (isUsernameAvailable)
            {
                user.Username = request.Username;
            }
            else
            {
                throw new InvalidOperationException("Username already taken");
            }
        }

        if (!string.IsNullOrEmpty(request.Bio))
            user.Bio = request.Bio;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
