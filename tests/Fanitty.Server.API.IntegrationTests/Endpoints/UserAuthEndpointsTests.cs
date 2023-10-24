using Fanitty.Server.API.IntegrationTests.Base;
using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Fanitty.Server.API.IntegrationTests.Endpoints;

public class UserAuthEndpointsTests : UserNotCreatedBase
{
    [Fact]
    public async Task Post_UserNotExists_CreateUser()
    {
        // Arrange
        // Act
        var response = await httpClient.PostAsync($"users", new StringContent(string.Empty, Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
        using var scope = webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<FanittyDbContext>();
        var userCount = dbContext.Users.Count();
        Assert.Equal(1, userCount);
    }
}
