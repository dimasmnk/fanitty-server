using Fanitty.Server.Infrastructure.Services.Firebase;

namespace Fanitty.Server.API.Extensions;

public static class AuthorizationExtensions
{
    public static void AddConfiguredAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthConstants.CreatedUserPolicy, policy =>
                policy.RequireClaim(Constants.UserIdClaimName));
        });
    }
}
