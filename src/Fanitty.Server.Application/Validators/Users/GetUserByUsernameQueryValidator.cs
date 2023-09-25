using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Users;
public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
    public GetUserByUsernameQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .MinimumLength(UserSettings.UsernameMinLength)
            .MaximumLength(UserSettings.UsernameMaxLength);
    }
}
