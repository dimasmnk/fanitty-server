using Fanitty.Server.Application.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;

namespace Fanitty.Server.Infrastructure.Services.Firebase;

public class FirebaseService : IFirebaseService
{
    public FirebaseApp App { get; }
    public FirebaseAuth Auth { get; }

    public FirebaseService(IConfiguration configuration)
    {
        var projectId = configuration.GetValue<string>(Constants.FirebaseProjectIdSectionName);
        App = FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.GetApplicationDefault(),
            ProjectId = projectId,
        });

        Auth = FirebaseAuth.DefaultInstance;
    }

    public async Task SetUserIdClaim(string uid, long userId)
    {
        var user = await Auth.GetUserAsync(uid);
        var customClaims = user.CustomClaims;

        var claims = new Dictionary<string, object>(customClaims)
        {
            { Constants.UserIdClaimName, userId }
        };

        await Auth.SetCustomUserClaimsAsync(uid, claims);
    }
}
