using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fanitty.Server.API.Extensions;

public static class MigrationExtensions
{
    public static IApplicationBuilder ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var db = scope.ServiceProvider.GetService<FanittyDbContext>()!;
        db.Database.Migrate();
        return app;
    }
}
