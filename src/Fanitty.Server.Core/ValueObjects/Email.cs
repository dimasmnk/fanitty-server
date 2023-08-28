using Fanitty.Server.Core.ValidatorPools;
using FluentValidation;

namespace Fanitty.Server.Core.ValueObjects;

public record Email
{
    public static Email Empty { get => new(); }

    public string Value { get; private set; }

    public Email()
    {
        Value = string.Empty;
    }

    public Email(string value)
    {
        CommonValidatorPool.EmailValidator.ValidateAndThrow(value);
        Value = value;
    }
}
