namespace Vestis._02_Application.Common;

public class BusinessNotificationContext
{
    private readonly List<string> _notifications = new();
    public IReadOnlyCollection<string> Notifications => _notifications.AsReadOnly();
    public bool HasNotifications => _notifications.Any();

    public void Add(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            _notifications.Add(message);
    }

    public void AddRange(IEnumerable<string> messages)
    {
        if (messages != null && messages.Any())
            _notifications.AddRange(messages.Where(message => !string.IsNullOrWhiteSpace(message)));
    }
}
