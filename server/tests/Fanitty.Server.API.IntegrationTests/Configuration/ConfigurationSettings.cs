using Fanitty.Server.Core.ValueObjects;

namespace Fanitty.Server.API.IntegrationTests.Configuration;
public static class ConfigurationSettings
{
    public static Guid UserId { get; set; } = Guid.NewGuid();
    public static Guid UserUid { get; set; } = Guid.NewGuid();
    public static string Username { get; set; } = UserId.ToString().Replace("-", string.Empty);
    public static Email Email { get; set; } = new Email { Value = "test@test.com" };
    public static string JwtSecret { get; set; } = "secretsecretsecretsecretsecretsecretsecret";
    public static string Issuer { get; set; } = nameof(Issuer);
    public static string Audience { get; set; } = nameof(Audience);
    public static string AuthenticationScheme = "BearerTest";
}
