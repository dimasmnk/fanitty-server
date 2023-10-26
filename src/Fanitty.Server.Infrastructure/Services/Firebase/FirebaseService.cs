using Fanitty.Server.Application.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Fanitty.Server.Infrastructure.Services.Firebase;

public class FirebaseService : IFirebaseService
{
    public FirebaseApp App { get; }
    public FirebaseAuth Auth { get; }

    public FirebaseService(IConfiguration configuration)
    {
        var base64 = configuration.GetValue<string>("Firebase")!;
        var blob = Convert.FromBase64String(base64);
        var json = Encoding.UTF8.GetString(blob);
        App = FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromJson(json)
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

    public async Task<string> GetUserEmailByUid(string uid)
    {
        if (string.IsNullOrEmpty(uid))
        {
            throw new ArgumentException($"'{nameof(uid)}' cannot be null or empty.", nameof(uid));
        }

        var user = await Auth.GetUserAsync(uid);
        return user.Email;
    }
}
