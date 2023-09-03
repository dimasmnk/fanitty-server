using Fanitty.Server.Application.Commands.Users;
using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Validators.Users;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(UserSettings.UsernameMinLength)
            .MaximumLength(UserSettings.UsernameMaxLength)
            .When(x => !x.IsGeneratedUsername);
    }
}
