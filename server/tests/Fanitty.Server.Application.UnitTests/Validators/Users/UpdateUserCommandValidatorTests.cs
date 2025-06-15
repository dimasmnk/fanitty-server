﻿using AutoFixture.Xunit2;
using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.UnitTests.Extensions;
using Fanitty.Server.Application.Validators.Users;
using Fanitty.Server.Core.Settings;
using FluentAssertions;

namespace Fanitty.Server.Application.UnitTests.Validators.Users;
public class UpdateUserCommandValidatorTests
{
    private readonly UpdateUserCommandValidator _updateUserCommandValidator;

    public UpdateUserCommandValidatorTests()
    {
        _updateUserCommandValidator = new UpdateUserCommandValidator();
    }

    [Theory]
    [AutoData]
    public void Validate_AllPropertiesSetValidValues_ShouldReturnTrue(string username, string displayName, string bio)
    {
        // Arrange
        username = username.TrimToMaxLength(UserSettings.UsernameMinLength);
        displayName = displayName.TrimToMaxLength(UserSettings.DisplayNameMaxLength);
        bio = bio.TrimToMaxLength(UserSettings.BioMaxLength);

        var request = new UpdateUserCommand
        {
            Username = username,
            DisplayName = displayName,
            Bio = bio,
        };

        // Act
        var result = _updateUserCommandValidator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void Validate_SpecifiedInvalidUsername_ShouldReturnFalse(string displayName, string bio)
    {
        // Arrange
        var username = new string('a', UserSettings.UsernameMaxLength + 1);
        displayName = displayName.TrimToMaxLength(UserSettings.DisplayNameMaxLength);
        bio = bio.TrimToMaxLength(UserSettings.BioMaxLength);

        var request = new UpdateUserCommand
        {
            Username = username,
            DisplayName = displayName,
            Bio = bio,
        };

        // Act
        var result = _updateUserCommandValidator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public void Validate_InvalidDisplayName_ShouldReturnFalse(string username, string bio)
    {
        // Arrange
        username = username.TrimToMaxLength(UserSettings.UsernameMaxLength);
        var displayName = new string('a', UserSettings.DisplayNameMaxLength + 1);
        bio = bio.TrimToMaxLength(UserSettings.BioMaxLength);

        var request = new UpdateUserCommand
        {
            Username = username,
            DisplayName = displayName,
            Bio = bio,
        };

        // Act
        var result = _updateUserCommandValidator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public void Validate_InvalidBio_ShouldReturnFalse(string username, string displayName)
    {
        // Arrange
        username = username.TrimToMaxLength(UserSettings.UsernameMaxLength);
        displayName = displayName.TrimToMaxLength(UserSettings.DisplayNameMaxLength);
        var bio = new string('a', UserSettings.BioMaxLength + 1);

        var request = new UpdateUserCommand
        {
            Username = username,
            DisplayName = displayName,
            Bio = bio,
        };

        // Act
        var result = _updateUserCommandValidator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
