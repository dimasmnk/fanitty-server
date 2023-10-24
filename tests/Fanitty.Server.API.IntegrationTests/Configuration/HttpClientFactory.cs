using Fanitty.Server.Core.Entities;
using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Fanitty.Server.API.IntegrationTests.Configuration;
public class HttpClientFactory
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public HttpClientFactory(TestWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    public HttpClient CreateAuthenticatedUserHttpClient()
    {
        _webApplicationFactory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<FanittyDbContext>();
                try
                {
                    appContext.Users.Add(new User
                    {
                        Id = ConfigurationSettings.UserId,
                        Uid = ConfigurationSettings.UserUid.ToString(),
                        Username = ConfigurationSettings.Username,
                        DisplayName = ConfigurationSettings.Username,
                        Email = ConfigurationSettings.Email
                    });

                    appContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            });
        });

        return _webApplicationFactory.CreateClient();
    }

    public HttpClient CreateAuthenticatedWithoutUserHttpClient()
    {
        return _webApplicationFactory.CreateClient();
    }
}
