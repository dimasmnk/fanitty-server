using Fanitty.Server.Application.Interfaces;
using Fanitty.Server.Infrastructure.Services.Firebase;

namespace Fanitty.Server.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public int UserId
    {
        get
        {
            var claim = _httpContextAccessor?
                .HttpContext?
                .User
                .Claims
                .First(x => x.Type == Constants.UserIdClaimName).Value;

            return int.Parse(claim!);
        }
    }

    public string Uid
    {
        get
        {
            var claim = _httpContextAccessor?
                .HttpContext?
                .User
                .Claims
                .First(x => x.Type == Constants.UidClaimName).Value;

            return claim!;
        }
    }
}
