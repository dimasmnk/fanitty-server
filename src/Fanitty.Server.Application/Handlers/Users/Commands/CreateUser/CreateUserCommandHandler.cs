using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Entities;
using MediatR;

namespace Fanitty.Server.Application.Handlers.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IFanittyDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFirebaseService _firebaseService;

    public CreateUserCommandHandler(IFanittyDbContext context, IUserRepository userRepository, ICurrentUserService currentUserService, IFirebaseService firebaseService)
    {
        _context = context;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _firebaseService = firebaseService;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var uid = _currentUserService.Uid;
        var guid = Guid.NewGuid().ToString()[..7];
        var email = await _firebaseService.GetUserEmailByUidAsync(uid);
        var user = new User(guid + uid, request.Username + guid, email + guid);
        _userRepository.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        await _firebaseService.SetUserIdClaimAsync(uid, user.Id);
    }
}
