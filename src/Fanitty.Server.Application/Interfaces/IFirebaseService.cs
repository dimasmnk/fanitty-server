namespace Fanitty.Server.Application.Interfaces;
public interface IFirebaseService
{
    Task SetUserIdClaim(string uid, long userId);
}
