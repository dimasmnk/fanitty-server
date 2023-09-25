using Fanitty.Server.Application.Responses.Users;
using Fanitty.Server.Core.Entities;

namespace Fanitty.Server.Application.Mappers;
public static class UserMapperExtensions
{
    public static CurrentUserResponse MapToCurrentUserResponse(this User user)
    {
        return new CurrentUserResponse
        {
            Id = user.Id,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email.Value,
            Bio = user.Bio
        };
    }

    public static UserByUsernameResponse MapToUserByUsernameResponse(this User user)
    {
        return new UserByUsernameResponse
        {
            Id = user.Id,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Bio = user.Bio
        };
    }
}