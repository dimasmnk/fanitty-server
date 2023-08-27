using Fanitty.Server.Core.Interfaces.Persistence;
using Fanitty.Server.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fanitty.Server.Infrastructure.Persistence;

public class FanittyDbContext : DbContext, IFanittyDbContext
{
    private readonly IMediator _mediator;

    public FanittyDbContext(DbContextOptions<FanittyDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
