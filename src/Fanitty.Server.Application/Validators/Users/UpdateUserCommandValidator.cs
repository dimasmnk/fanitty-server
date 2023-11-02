using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Validators.Base;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Users;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .Username()
            .When(x => !string.IsNullOrEmpty(x.Username));

        RuleFor(x => x.DisplayName)
            .DisplayName()
            .When(x => !string.IsNullOrEmpty(x.DisplayName));

        RuleFor(x => x.Bio)
            .Bio()
            .When(x => !string.IsNullOrEmpty(x.Bio));
    }
}
