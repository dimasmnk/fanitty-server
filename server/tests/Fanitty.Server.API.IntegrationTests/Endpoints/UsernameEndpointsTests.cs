﻿using AutoFixture.Xunit2;
using Fanitty.Server.API.IntegrationTests.Base;
using Fanitty.Server.API.IntegrationTests.Extensions;
using Fanitty.Server.Application.Responses.Usernames;
using Fanitty.Server.Core.Entities;
using Fanitty.Server.Core.Settings;
using Fanitty.Server.Core.ValueObjects;
using Fanitty.Server.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Fanitty.Server.API.IntegrationTests.Endpoints;

public class UsernameEndpointsTests : UserAuthenticatedBase
{
    [Theory]
    [AutoData]
    public async Task Get_UsernameIsAvailable_ShouldReturnTrue(string username)
    {
        // Arrange
        username = username.TrimToMaxLength(UserSettings.UsernameMaxLength);

        // Act
        var response = await httpClient.GetAsync($"usernames/check/{username}");

        // Assert
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadFromJsonAsync<CheckUsernameAvailabilityResponse>();
        Assert.True(data?.IsAvailable);
    }

    [Theory]
    [AutoData]
    public async Task Get_UsernameIsTaken_ShouldReturnFalse(string username)
    {
        // Arrange
        username = username.TrimToMaxLength(UserSettings.UsernameMaxLength);
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
