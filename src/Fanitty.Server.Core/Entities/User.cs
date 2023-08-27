using Fanitty.Server.Core.Common;
using Fanitty.Server.Core.ValidatorPools;
using Fanitty.Server.Core.ValueObjects;
using FluentValidation;

namespace Fanitty.Server.Core.Entities;

public class User : BaseEntity
{
    public long Id { get; set; }
    public string Uuid { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = Email.Empty;
    public string? Bio { get; private set; }

    public User(string uuid, string username, string email)
    {
        Uuid = uuid;
        UserValidatorPool.UsernameValidator.ValidateAndThrow(username);
        Username = username;
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
