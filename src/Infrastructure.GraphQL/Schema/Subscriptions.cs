namespace Kathanika.Infrastructure.GraphQL.Schema;

public class Notification
{
    public string? Message { get; set; }
}

public class Subscriptions
{
    [Subscribe]
    [Topic("NewNotification")]
    public Notification OnNewNotification([EventMessage] Notification notification)
    {
        return notification;
    }
}