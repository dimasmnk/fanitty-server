using Fanitty.Server.Core.Settings;
using FluentValidation;

namespace Fanitty.Server.Application.Handlers.Users.Commands.CreateUser;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(UserSettings.UsernameMinLength)
            .MaximumLength(UserSettings.UsernameMaxLength);
    }
}
