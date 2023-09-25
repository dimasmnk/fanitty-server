using Fanitty.Server.Core.Entities;

namespace Fanitty.Server.Application.Interfaces.Persistence.IRepositories;

public interface IUserRepository : IBaseRepositoryGeneric<User>
{
    Task<User> GetUserByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> IsUsernameAvailableAsync(string username, CancellationToken cancellationToken);
    Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
}
