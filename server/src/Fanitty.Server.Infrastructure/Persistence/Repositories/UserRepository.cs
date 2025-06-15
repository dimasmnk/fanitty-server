using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Fanitty.Server.Infrastructure.Persistence.Repositories;
public class UserRepository : BaseRepositoryGeneric<User>, IUserRepository
{
    public UserRepository(FanittyDbContext context) : base(context) { }

    public async Task<User> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await entity.FindAsync(new object[] { id }, cancellationToken);
        return user ?? throw new UserNotFoundException($"User with id {id} not found.");
    }

    public async Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var user = await entity.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        return user ?? throw new UserNotFoundException($"User with username {username} not found.");
    }

    public async Task<bool> IsUsernameAvailableAsync(string username, CancellationToken cancellationToken)
    {
        return !(await entity.AnyAsync(x => x.Username == username, cancellationToken));
    }
}
