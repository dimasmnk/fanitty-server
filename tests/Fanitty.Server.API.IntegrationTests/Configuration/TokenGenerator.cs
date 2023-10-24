using Fanitty.Server.Infrastructure.Services.Firebase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fanitty.Server.API.IntegrationTests.Configuration;

public static class TokenGenerator
{
    public static string GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationSettings.JwtSecret));
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = ConfigurationSettings.Issuer,
            Audience = ConfigurationSettings.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var claims = new List<Claim>
        {
            new Claim(Constants.UserIdClaimName, ConfigurationSettings.UserId.ToString()),
            new Claim(Constants.UidClaimName, ConfigurationSettings.UserUid.ToString()),
        };

        tokenDescriptor.Subject = new ClaimsIdentity(claims);
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static string GenerateTokenWithoutUserId()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationSettings.JwtSecret));
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = ConfigurationSettings.Issuer,
            Audience = ConfigurationSettings.Audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var claims = new List<Claim>
        {
            new Claim(Constants.UidClaimName, ConfigurationSettings.UserUid.ToString()),
        };

        tokenDescriptor.Subject = new ClaimsIdentity(claims);
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
