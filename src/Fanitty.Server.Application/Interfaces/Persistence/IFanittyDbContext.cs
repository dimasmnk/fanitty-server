namespace Fanitty.Server.Application.Interfaces.Persistence;

public interface IFanittyDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
