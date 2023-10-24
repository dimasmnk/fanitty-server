using Fanitty.Server.API.IntegrationTests.Base;
using Fanitty.Server.API.IntegrationTests.Configuration;
using Fanitty.Server.Application.Responses.Users;
using Fanitty.Server.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Fanitty.Server.API.IntegrationTests.Endpoints;

public class UserEndpointsTests : UserAuthenticatedBase
{
    [Fact]
    public async Task Get_CurrentUserExists_ReturnCurrentUser()
    {
        // Arrange
        using var scope = webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<FanittyDbContext>();
        var userCount = dbContext.Users.FirstOrDefault();

        // Act
        var response = await httpClient.GetAsync("users/me");

        // Assert
        response.EnsureSuccessStatusCode();
        var data = response.Content.ReadFromJsonAsync<CurrentUserResponse>();
        Assert.NotNull(data.Result);
        data.Result.Id.Should().Be(ConfigurationSettings.UserId);
    }
}
