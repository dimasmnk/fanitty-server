using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Core.Validators.User;

public class DisplayNameValidator : AbstractValidator<string>
{
    public DisplayNameValidator()
    {
        RuleFor(x => x)
            .MaximumLength(UserSettings.DisplayNameMaxLength);
    }
}
