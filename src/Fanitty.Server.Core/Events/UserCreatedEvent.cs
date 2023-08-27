using Fanitty.Server.Core.Common;
using Fanitty.Server.Core.Entities;

namespace Fanitty.Server.Core.Events;
public class UserCreatedEvent : BaseEvent
{
    public User User { get; }

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}
