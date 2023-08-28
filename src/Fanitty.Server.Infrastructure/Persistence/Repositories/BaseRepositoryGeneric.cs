using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Core.Common;
using Microsoft.EntityFrameworkCore;

namespace Fanitty.Server.Infrastructure.Persistence.Repositories;

public class BaseRepositoryGeneric<TEntity> : IBaseRepositoryGeneric<TEntity> where TEntity : BaseEntity
{
    protected readonly FanittyDbContext context;
    protected DbSet<TEntity> entity;
    private bool disposed = false;

    public BaseRepositoryGeneric(FanittyDbContext context)
    {
        this.context = context;
        entity = this.context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        this.entity.Add(entity);
    }

    public void AddRange(List<TEntity> entity)
    {
        this.entity.AddRange(entity);
    }

    public void Remove(TEntity entity)
    {
        this.entity.Remove(entity);
    }

    public void UpdateAsync(TEntity entity)
    {
        this.entity.Update(entity);
    }

    public void UpdateRangeAsync(List<TEntity> entity)
    {
        this.entity.UpdateRange(entity);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
