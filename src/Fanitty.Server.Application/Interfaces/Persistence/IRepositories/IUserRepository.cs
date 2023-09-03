using Fanitty.Server.Core.Entities;

namespace Fanitty.Server.Application.Interfaces.Persistence.IRepositories;

public interface IUserRepository : IBaseRepositoryGeneric<User>
{
    Task<User?> GetUserById(long id, CancellationToken cancellationToken);
}
