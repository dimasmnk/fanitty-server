using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Core.Validators.User;

public class UsernameValidator : AbstractValidator<string>
{
    public UsernameValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .MinimumLength(UserSettings.UsernameMinLength)
            .MaximumLength(UserSettings.UsernameMaxLength);
    }
}
