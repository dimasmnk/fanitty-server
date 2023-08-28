using Fanitty.Server.Core.Common;

namespace Fanitty.Server.Application.Interfaces.Persistence.IRepositories;

public interface IBaseRepositoryGeneric<TEntity> : IDisposable where TEntity : BaseEntity
{
    void Add(TEntity entity);
    void AddRange(List<TEntity> entity);
    void Remove(TEntity entity);
    void UpdateAsync(TEntity entity);
    void UpdateRangeAsync(List<TEntity> entity);
}
