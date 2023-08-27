using FluentValidation;

namespace Fanitty.Server.Core.Validators.Common;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .EmailAddress();
    }
}
