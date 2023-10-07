using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Application.Validators.Base;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Users;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .Username()
            .When(x => !x.IsGeneratedUsername);
    }
}
