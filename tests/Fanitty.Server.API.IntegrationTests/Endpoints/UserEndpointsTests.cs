using AutoFixture.Xunit2;
using Fanitty.Server.API.IntegrationTests.Base;
using Fanitty.Server.API.IntegrationTests.Configuration;
using Fanitty.Server.API.IntegrationTests.Extensions;
using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Responses.Users;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Settings;
using Fanitty.Server.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Fanitty.Server.API.IntegrationTests.Endpoints;

public class UserEndpointsTests : UserAuthenticatedBase
{
    [Fact]
    public async Task Get_CurrentUserExists_ReturnCurrentUser()
    {
        // Arrange
        // Act
        var response = await httpClient.GetAsync("users/me");

        // Assert
        response.EnsureSuccessStatusCode();
        var data = response.Content.ReadFromJsonAsync<CurrentUserResponse>();
        Assert.NotNull(data.Result);
        data.Result.Id.Should().Be(ConfigurationSettings.UserId);
    }

    [Theory]
    [AutoData]
    public async Task Get_UserByUsernameAndUserExists_ReturnUser(User user)
    {
        // Arrange
        user.Username = user.Username.TrimToMaxLength(UserSettings.UsernameMaxLength);
        user.DisplayName = user.Username;
        using var scope = webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<FanittyDbContext>();
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        // Act
        var response = await httpClient.GetAsync($"users/{user.Username}");

        // Assert
        response.EnsureSuccessStatusCode();
        var data = response.Content.ReadFromJsonAsync<UserByUsernameResponse>();
        Assert.NotNull(data.Result);
        data.Result.Id.Should().Be(user.Id);
    }

    [Theory]
    [AutoData]
    public async Task Put_UserExists_ShouldUpdateUser(User user)
    {
        // Arrange
        user.Username = user.Username.TrimToMaxLength(UserSettings.UsernameMaxLength);
        user.DisplayName = user.Username;
        var updateUserCommand = new UpdateUserCommand
        {
            Username = user.Username,
            DisplayName = user.Username,
            Bio = user.Bio
        };

        // Act
        var response = await httpClient.PutAsync("users", new StringContent(JsonSerializer.Serialize(updateUserCommand), Encoding.UTF8, MediaTypeNames.Application.Json));

        // Assert
        response.EnsureSuccessStatusCode();
        using var scope = webApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var dbContext = scopedServices.GetRequiredService<FanittyDbContext>();
        var data = dbContext.Users.Find(ConfigurationSettings.UserId)!;
        data.Username.Should().Be(user.Username);
        data.DisplayName.Should().Be(user.DisplayName);
        data.Bio.Should().Be(user.Bio);
    }
}
