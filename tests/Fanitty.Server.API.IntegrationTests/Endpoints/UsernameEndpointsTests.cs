using Fanitty.Server.API.IntegrationTests.Base;
using Fanitty.Server.Application.Responses.Usernames;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.ValueObjects;
using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Fanitty.Server.API.IntegrationTests.Endpoints;
public class UsernameEndpointsTests : UserAuthenticatedBase
{
    public UsernameEndpointsTests()
    {
    }

    [Fact]
    public async Task Username_UsernameIsAvailable_ShouldReturnTrue()
    {
        // Arrange
        var username = "aasdfsdf";

        // Act
        var response = await httpClient.GetAsync($"usernames/check/{username}");

        // Assert
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<CheckUsernameAvailabilityResponse>();
        Assert.True(data?.IsAvailable);
    }

    [Fact]
    public async Task Username_UsernameIsTaken_ShouldReturnFalse()
    {
        // Arrange
        var username = "aasdfsdf";
        using var scope = webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<FanittyDbContext>();
        dbContext.Add(new User
        {
            Uid = username,
            Username = username,
            DisplayName = username,
            Email = new Email { Value = "asdf@asdf.com" }
        });
        dbContext.SaveChanges();

        // Act
        var response = await httpClient.GetAsync($"usernames/check/{username}");

        // Assert
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<CheckUsernameAvailabilityResponse>();
        Assert.False(data?.IsAvailable);
    }
}
