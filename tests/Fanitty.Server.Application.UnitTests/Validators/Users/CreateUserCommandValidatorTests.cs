using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Validators.Users;
using FluentAssertions;

namespace Fanitty.Server.Application.UnitTests.Validators.Users;
public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _createUserCommandValidator;

    public CreateUserCommandValidatorTests()
    {
        _createUserCommandValidator = new CreateUserCommandValidator();
    }

    [Fact]
    public void Validate_GeneratedUsername_ShouldSkipUsernameAndReturnTrue()
    {
        // Arrange
        var username = "usernameusernameusernameusernameusername123";
        var createUserCommand = new CreateUserCommand
        {
            IsGeneratedUsername = true,
            Username = username
        };

        // Act
        var result = _createUserCommandValidator.Validate(createUserCommand);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_SpecifiedValidUsername_ShouldReturnTrue()
    {
        // Arrange
        var username = "username123";
        var createUserCommand = new CreateUserCommand
        {
            IsGeneratedUsername = false,
            Username = username
        };

        // Act
        var result = _createUserCommandValidator.Validate(createUserCommand);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_SpecifiedInvalidUsername_ShouldReturnFalse()
    {
        // Arrange
        var username = "usernameusernameusernameusernameusername123";
        var createUserCommand = new CreateUserCommand
        {
            IsGeneratedUsername = false,
            Username = username
        };

        // Act
        var result = _createUserCommandValidator.Validate(createUserCommand);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
