using MediatR;

namespace SmartSalon.Application.Notifications;

public class ChangedNotification : INotification
{
    public Id Id { get; set; }
}