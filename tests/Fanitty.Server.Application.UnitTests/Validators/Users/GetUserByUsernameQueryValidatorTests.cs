using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Application.Validators.Users;
using FluentAssertions;

namespace Fanitty.Server.Application.UnitTests.Validators.Users;
public class GetUserByUsernameQueryValidatorTests
{
    private readonly GetUserByUsernameQueryValidator _getUserByUsernameQueryValidator;

    public GetUserByUsernameQueryValidatorTests()
    {
        _getUserByUsernameQueryValidator = new GetUserByUsernameQueryValidator();
    }

    [Fact]
    public void Validate_ValidUsername_ShouldReturnTrue()
    {
        // Arrange
        var username = "username";
        var request = new GetUserByUsernameQuery(username);

        // Act
        var result = _getUserByUsernameQueryValidator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidUsername_ShouldReturnFalse()
    {
        // Arrange
        var username = "usernameusernameusernameusernameusernameusername";
        var request = new GetUserByUsernameQuery(username);

        // Act
        var result = _getUserByUsernameQueryValidator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
