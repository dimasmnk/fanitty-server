using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Settings;
using Fanitty.Server.Core.ValueObjects;
using MediatR;

namespace Fanitty.Server.Application.Handlers.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IFanittyDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFirebaseService _firebaseService;
    private readonly IUsernameGeneratorService _usernameGeneratorService;

    public CreateUserCommandHandler(IFanittyDbContext context, IUserRepository userRepository, ICurrentUserService currentUserService, IFirebaseService firebaseService, IUsernameGeneratorService usernameGeneratorService)
    {
        _context = context;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _firebaseService = firebaseService;
        _usernameGeneratorService = usernameGeneratorService;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var uid = _currentUserService.GetUid();
        var email = await _firebaseService.GetUserEmailByUidAsync(uid);
        var username = request.IsGeneratedUsername
            ? _usernameGeneratorService.GenerateUsernameFromEmail(email, 4, UserSettings.UsernameMaxLength)
            : request.Username;
        var user = new User
        {
            Uid = uid,
            Username = username,
            DisplayName = username,
            Email = new Email { Value = email }
        };
        _userRepository.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        await _firebaseService.SetUserIdClaimAsync(uid, user.Id);
    }
}
