namespace Fanitty.Server.Application.Interfaces;
public interface IFirebaseService
{
    Task AddUserIdClaim(string uid, long userId);
}
