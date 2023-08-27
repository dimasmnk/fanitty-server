using Fanitty.Server.Core.Common;
using Fanitty.Server.Core.ValidatorPools;
using Fanitty.Server.Core.ValueObjects;
using FluentValidation;

namespace Fanitty.Server.Core.Entities;

public class User : BaseEntity
{
    public long Id { get; private set; }
    public string? Uuid { get; private set; }
    public string? Username { get; private set; }
    public string? DisplayName { get; private set; }
    public Email? Email { get; private set; }
    public string? Bio { get; private set; }

    public User() { }

    public User(string uuid, string username, string email)
    {
        Uuid = uuid;
        UserValidatorPool.UsernameValidator.ValidateAndThrow(username);
        Username = username;
        DisplayName = username;
        Email = new Email(email);
    }

    public void UpdateUuid(string uuid)
    {
        Uuid = uuid;
    }

    public void UpdateUsername(string username)
    {
        UserValidatorPool.UsernameValidator.ValidateAndThrow(username);
        Username = username;
    }

    public void UpdateDisplayName(string displayName)
    {
        UserValidatorPool.DisplayNameValidator.ValidateAndThrow(displayName);
        DisplayName = displayName;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void UpdateBio(string bio)
    {
        UserValidatorPool.BioValidator.ValidateAndThrow(bio);
        Bio = bio;
    }
}
