using Fanitty.Server.Core.Validators.Common;

namespace Fanitty.Server.Core.ValidatorPools;
public static class CommonValidatorPool
{
    private static readonly Lazy<EmailValidator> _emailValidator = new(() => new EmailValidator(), LazyThreadSafetyMode.PublicationOnly);
    public static EmailValidator EmailValidator => _emailValidator.Value;
}
