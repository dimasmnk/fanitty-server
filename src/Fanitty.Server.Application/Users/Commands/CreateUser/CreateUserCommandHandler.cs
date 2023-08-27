using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Interfaces.Persistence;
using Fanitty.Server.Core.Interfaces.Persistence.IRepositories;
using MediatR;

namespace Fanitty.Server.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IFanittyDbContext _context;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IFanittyDbContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User("asdf", "Test", "semenkodima97@gmail.com");
        _userRepository.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
