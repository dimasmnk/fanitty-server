using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure.Services.Firebase;

namespace Fanitty.Server.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public Guid? UserId
    {
        get
        {
            var claim = _httpContextAccessor?
                .HttpContext?
                .User
                .Claims
                .FirstOrDefault(x => x.Type == Constants.UserIdClaimName)?.Value;

            return claim is null
                ? null
                : Guid.Parse(claim);
        }
    }

    public string? Uid
    {
        get
        {
            var claim = _httpContextAccessor?
                .HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(x => x.Type == Constants.UidClaimName)?.Value;

            return claim!;
        }
    }

    public Guid GetUserId()
    {
        if (UserId == null)
        {
            throw new NullReferenceException("User id is not set in user claims.");
        }
        else
        {
            return UserId.Value;
        }
    }
}
