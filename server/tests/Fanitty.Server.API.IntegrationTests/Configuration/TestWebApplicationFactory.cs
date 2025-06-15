using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure.Persistence;
using Fanitty.Server.Infrastructure.Services.Firebase;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Fanitty.Server.API.IntegrationTests.Configuration;
public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private readonly string _dbName;

    public TestWebApplicationFactory(string dbName)
    {
        _dbName = dbName;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ConfigurationSettings.AuthenticationScheme;
                options.DefaultChallengeScheme = ConfigurationSettings.AuthenticationScheme;
                options.DefaultScheme = ConfigurationSettings.AuthenticationScheme;
            })
            .AddJwtBearer(ConfigurationSettings.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = ConfigurationSettings.Audience,
                    ValidIssuer = ConfigurationSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationSettings.JwtSecret))
                };
            });

            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<FanittyDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<FanittyDbContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<FanittyDbContext>();
            try
            {
                appContext.Database.EnsureCreated();
            }
            catch (Exception)
            {
                throw;
            }

            var firebaseDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(FirebaseService));

            if (firebaseDescriptor != null)
                services.Remove(firebaseDescriptor);

            services.AddSingleton<IFirebaseService, MockFirebaseService>();
        });
    }
}
