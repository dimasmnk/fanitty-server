using Fanitty.Server.API.IntegrationTests.Configuration;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Fanitty.Server.API.IntegrationTests.Base;

public class UserAuthenticatedBase
{
    protected TestWebApplicationFactory<Program> webApplicationFactory;
    protected HttpClient httpClient;

    public UserAuthenticatedBase()
    {
        webApplicationFactory = new TestWebApplicationFactory<Program>(Guid.NewGuid().ToString());
        httpClient = webApplicationFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, TokenGenerator.GenerateToken());

        using var scope = webApplicationFactory.Services.CreateScope();
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
    }
}