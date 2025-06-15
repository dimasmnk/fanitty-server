using Fanitty.Server.Core.Common;
using Fanitty.Server.Core.ValueObjects;

namespace Fanitty.Server.Core.Entities;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public string Uid { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Email Email { get; set; } = new Email();
    public string? Bio { get; set; }
}
