using Fanitty.Server.Infrastructure.Services.Firebase;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Fanitty.Server.API.Extensions;

public static class FirebaseAuthenticationExtensions
{
    public static void AddFirebaseAuthentication(this WebApplicationBuilder builder)
    {
        var audience = builder.Configuration.GetValue<string>(Constants.FirebaseProjectIdSectionName);
        builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RefreshOnIssuerKeyNotFound = true;
            o.MetadataAddress = $"https://securetoken.google.com/{audience}/.well-known/openid-configuration";
            o.Audience = audience;
        });
    }
}
