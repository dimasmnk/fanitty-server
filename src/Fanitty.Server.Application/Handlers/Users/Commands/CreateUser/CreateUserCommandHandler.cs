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

    public CreateUserCommandHandler(IFanittyDbContext context, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _context = context;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var uid = _currentUserService.Uid;
        var user = new User(request.Uid, request.Username, request.Email);
        _userRepository.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
