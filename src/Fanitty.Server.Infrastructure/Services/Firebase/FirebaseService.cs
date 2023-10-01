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
        var path = configuration.GetValue<string>("Firebase:CredentialsFilePath");
        App = FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(path)
        });

        Auth = FirebaseAuth.DefaultInstance;
    }

    public async Task SetUserIdClaimAsync(string uid, Guid userId)
    {
        var user = await Auth.GetUserAsync(uid);
        var customClaims = user.CustomClaims;

        var claims = new Dictionary<string, object>(customClaims)
        {
            { Constants.UserIdClaimName, userId }
        };

        await Auth.SetCustomUserClaimsAsync(uid, claims);
    }

    public async Task<string> GetUserEmailByUidAsync(string uid)
    {
        if (string.IsNullOrEmpty(uid))
        {
            throw new ArgumentException($"'{nameof(uid)}' cannot be null or empty.", nameof(uid));
        }

        var user = await Auth.GetUserAsync(uid);
        return user.Email;
    }
}
