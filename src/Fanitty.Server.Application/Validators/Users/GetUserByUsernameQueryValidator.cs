using Fanitty.Server.Application.Queries.Users;
using Fanitty.Server.Application.Validators.Base;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Users;
public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
    public GetUserByUsernameQueryValidator()
    {
        RuleFor(x => x.Username)
            .Username();
    }
}
