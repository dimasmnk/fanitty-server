using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Application.Interfaces.Persistence;
using Fanitty.Server.Application.Interfaces.Persistence.IRepositories;
using Fanitty.Server.Infrastructure.Persistence;
using Fanitty.Server.Infrastructure.Persistence.Repositories;
using Fanitty.Server.Infrastructure.Services.Firebase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fanitty.Server.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FanittyDbContext>(options =>
        options.UseNpgsql(configuration.GetValue<string>("Database"),
        builder => builder.MigrationsAssembly(typeof(FanittyDbContext).Assembly.FullName)));

        services.AddScoped<IFanittyDbContext>(provider => provider.GetRequiredService<FanittyDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IFirebaseService, FirebaseService>();

        return services;
    }
}
