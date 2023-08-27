namespace Fanitty.Server.Core.Interfaces.Persistence;

public interface IFanittyDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
