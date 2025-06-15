namespace Fanitty.Server.Application.Interfaces;
public interface IFirebaseService
{
    Task SetUserIdClaimAsync(string uid, Guid userId);
    Task<string> GetUserEmailByUid(string uid);
}
