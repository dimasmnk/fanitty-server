namespace Fanitty.Server.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Uid { get; }
    Guid GetUserId();
    string GetUid();
}
