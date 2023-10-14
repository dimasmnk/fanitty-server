using AutoFixture.Xunit2;
using Fanitty.Server.Application.Services;
using FluentAssertions;
using System.Net.Mail;

namespace Fanitty.Server.Application.UnitTests.Services;
public class UsernameGeneratorServiceTests
{
    private readonly UsernameGeneratorService _usernameGeneratorService;

    public UsernameGeneratorServiceTests()
    {
        _usernameGeneratorService = new UsernameGeneratorService();
    }

    [Theory]
    [AutoData]
    public void GenerateUsernameFromEmail_ValidEmail_ShouldReturnGeneratedUsername(MailAddress mail)
    {
        // Arrange
        var email = mail.Address;
        var digitCount = 4;
        var maxLength = 50;

        // Act
        var username = _usernameGeneratorService.GenerateUsernameFromEmail(email, digitCount, maxLength);

        //Assert
        var actualDigitCount = username.Length - mail.User.Length;
        actualDigitCount.Should().Be(digitCount);
    }
}
