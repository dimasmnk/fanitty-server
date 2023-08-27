using Fanitty.Server.Core.Validators.User;

namespace Fanitty.Server.Core.ValidatorPools;

public static class UserValidatorPool
{
    private static readonly Lazy<UsernameValidator> _usernameValidator = new(() => new UsernameValidator(), LazyThreadSafetyMode.PublicationOnly);
    private static readonly Lazy<BioValidator> _bioValidator = new(() => new BioValidator(), LazyThreadSafetyMode.PublicationOnly);
    private static readonly Lazy<DisplayNameValidator> _displayNameValidator = new(() => new DisplayNameValidator(), LazyThreadSafetyMode.PublicationOnly);

    public static UsernameValidator UsernameValidator => _usernameValidator.Value;
    public static BioValidator BioValidator => _bioValidator.Value;
    public static DisplayNameValidator DisplayNameValidator => _displayNameValidator.Value;
}
