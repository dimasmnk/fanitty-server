namespace Fanitty.Server.Application.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    string Uid { get; }
}
