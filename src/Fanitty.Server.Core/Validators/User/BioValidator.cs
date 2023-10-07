using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Core.Validators.User;

public class BioValidator : AbstractValidator<string?>
{
    public BioValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .MaximumLength(UserSettings.BioMaxLength);
    }
}
