using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Base;
public static class BioValidator
{
    public static IRuleBuilderOptions<T, string?> Bio<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .MaximumLength(UserSettings.BioMaxLength);
    }
}
