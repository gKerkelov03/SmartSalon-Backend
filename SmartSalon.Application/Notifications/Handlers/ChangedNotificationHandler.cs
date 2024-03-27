using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace SmartSalon.Application.Notifications.Handlers;

public class ChangedNotificationHandler : INotificationHandler<ChangedNotification>
{
    private readonly IDistributedCache _cache;

    public ChangedNotificationHandler(IDistributedCache cache)
        => _cache = cache;

    public async Task Handle(ChangedNotification notification, CancellationToken cancellationToken)
        => await _cache.RemoveAsync(notification.Id.ToString());
}