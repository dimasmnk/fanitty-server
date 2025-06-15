using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Base;
public static class UsernameValidator
{
    public static IRuleBuilderOptions<T, string?> Username<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .MinimumLength(UserSettings.UsernameMinLength)
            .MaximumLength(UserSettings.UsernameMaxLength);
    }
}
