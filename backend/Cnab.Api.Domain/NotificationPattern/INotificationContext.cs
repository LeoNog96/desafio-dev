using FluentValidation.Results;

namespace Cnab.Api.Domain.NotificationPattern
{
    public interface INotificationContext
    {
        public IReadOnlyCollection<Notification> Notifications { get; }
        public bool HasNotifications { get; }
        public int StatusCode { get; }
        void SetStatusCode(int statusCode);
        void AddNotification(string key, string message);
        void AddNotification(Notification notification);
        void AddNotifications(IEnumerable<Notification> notifications);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
