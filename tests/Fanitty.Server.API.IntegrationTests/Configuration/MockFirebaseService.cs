using Fanitty.Server.Application.Interfaces;

namespace Fanitty.Server.API.IntegrationTests.Configuration;
public class MockFirebaseService : IFirebaseService
{
    public async Task<string> GetUserEmailByUid(string uid)
    {
        return await Task.FromResult(ConfigurationSettings.Email.Value);
    }

    public async Task SetUserIdClaimAsync(string uid, Guid userId)
    {
        await Task.CompletedTask;
    }
}
