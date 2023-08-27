using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Interfaces.Persistence.IRepositories;

namespace Fanitty.Server.Infrastructure.Persistence.Repositories;
public class UserRepository : BaseRepositoryGeneric<User>, IUserRepository
{
    public UserRepository(FanittyDbContext context) : base(context) { }
}
