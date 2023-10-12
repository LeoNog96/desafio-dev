using Newtonsoft.Json;

namespace Cnab.Api.Domain.NotificationPattern
{
    public class NotificationResponse
    {
        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("notifications")]
        public IEnumerable<Notification> Notifications { get; private set; }

        public NotificationResponse(string title, IEnumerable<Notification> notifications)
        {
            Title = title;
            Notifications = notifications;
        }
    }
}
