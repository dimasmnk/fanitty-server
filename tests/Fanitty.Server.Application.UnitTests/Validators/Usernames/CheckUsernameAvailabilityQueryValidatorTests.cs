using Fanitty.Server.Application.Queries.Usernames;
using Fanitty.Server.Application.Validators.Usernames;
using FluentAssertions;

namespace Fanitty.Server.Application.UnitTests.Validators.Usernames;
public class CheckUsernameAvailabilityQueryValidatorTests
{
    private readonly CheckUsernameAvailabilityQueryValidator _checkUsernameAvailabilityQueryValidator;

    public CheckUsernameAvailabilityQueryValidatorTests()
    {
        _checkUsernameAvailabilityQueryValidator = new CheckUsernameAvailabilityQueryValidator();
    }

    [Fact]
    public void Validate_ValidUsername_ShouldBeValid()
    {
        // Arrange
        var username = "username";

        var query = new CheckUsernameAvailabilityQuery(username);

        // Act
        var result = _checkUsernameAvailabilityQueryValidator.Validate(query);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_InvalidUsername_ShouldBeInvalid()
    {
        // Arrange
        var username = "usernameusernameusernameusernameusername123";

        var query = new CheckUsernameAvailabilityQuery(username);

        // Act
        var result = _checkUsernameAvailabilityQueryValidator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
