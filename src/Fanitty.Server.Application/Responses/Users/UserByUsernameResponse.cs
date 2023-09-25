namespace Fanitty.Server.Application.Responses.Users;
public class UserByUsernameResponse
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Bio { get; set; }
}
