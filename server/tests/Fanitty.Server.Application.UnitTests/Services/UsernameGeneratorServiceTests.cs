using Fanitty.Server.Application.Services.UsernameGenerator;
using Fanitty.Server.Core.Settings;
using FluentAssertions;

namespace Fanitty.Server.Application.UnitTests.Services;
public class UsernameGeneratorServiceTests
{
    private readonly UsernameGeneratorService _usernameGeneratorService;

    public UsernameGeneratorServiceTests()
    {
        _usernameGeneratorService = new UsernameGeneratorService();
    }

    [Fact]
    public void GenerateUsername_NoInput_ShouldReturnGeneratedUsername()
    {
        // Arrange
        var maxLength = UserSettings.UsernameMaxLength;

        // Act
        var username = _usernameGeneratorService.GenerateUsername();

        //Assert
        username.Length.Should().BeLessThan(maxLength);
    }
}
