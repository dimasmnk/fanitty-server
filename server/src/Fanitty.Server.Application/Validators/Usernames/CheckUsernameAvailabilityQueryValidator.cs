using Fanitty.Server.Application.Queries.Usernames;
using Fanitty.Server.Application.Validators.Base;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Usernames;
public class CheckUsernameAvailabilityQueryValidator : AbstractValidator<CheckUsernameAvailabilityQuery>
{
    public CheckUsernameAvailabilityQueryValidator()
    {
        RuleFor(x => x.Username)
            .Username();
    }
}
