namespace Fanitty.Server.Application.Interfaces;
public interface IFirebaseService
{
    Task SetUserIdClaimAsync(string uid, long userId);
    Task<string> GetUserEmailByUidAsync(string uid);
}
