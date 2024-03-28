using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace SmartSalon.Application.Notifications.Handlers;

internal class ChangedNotificationHandler(IDistributedCache _cache) : INotificationHandler<ChangedNotification>
{
    public async Task Handle(ChangedNotification notification, CancellationToken cancellationToken)
        => await _cache.RemoveAsync(notification.Id.ToString());
}