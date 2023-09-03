using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Entities;

namespace Fanitty.Server.Infrastructure.Persistence.Repositories;
public class UserRepository : BaseRepositoryGeneric<User>, IUserRepository
{
    public UserRepository(FanittyDbContext context) : base(context) { }

    public async Task<User?> GetUserById(long id, CancellationToken cancellationToken)
    {
        return await entity.FindAsync(new object[] { id }, cancellationToken);
    }
}
