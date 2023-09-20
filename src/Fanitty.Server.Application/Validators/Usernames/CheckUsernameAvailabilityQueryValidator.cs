using Fanitty.Server.Application.Queries.Usernames;
using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Usernames;
public class CheckUsernameAvailabilityQueryValidator : AbstractValidator<CheckUsernameAvailabilityQuery>
{
    public CheckUsernameAvailabilityQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .MinimumLength(UserSettings.UsernameMinLength)
            .MaximumLength(UserSettings.UsernameMaxLength);
    }
}
