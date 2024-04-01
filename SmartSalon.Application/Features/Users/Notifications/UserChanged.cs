using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace SmartSalon.Application.Features.Users.Notifications;

public class UserChangedNotification : INotification
{
    public Id Id { get; set; }
}

internal class ChangedNotificationHandler(IDistributedCache _cache) : INotificationHandler<UserChangedNotification>
{
    public async Task Handle(UserChangedNotification notification, CancellationToken cancellationToken)
        => await _cache.RemoveAsync(notification.Id.ToString());
}