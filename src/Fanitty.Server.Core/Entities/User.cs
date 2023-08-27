namespace Fanitty.Server.Core.Entities;

public class User
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }
}
