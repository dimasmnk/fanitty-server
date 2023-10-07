using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Base;
public static class DisplayNameValidator
{
    public static IRuleBuilderOptions<T, string?> DisplayName<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .MaximumLength(UserSettings.DisplayNameMaxLength);
    }
}
