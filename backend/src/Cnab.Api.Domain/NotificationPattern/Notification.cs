using Newtonsoft.Json;

namespace Cnab.Api.Domain.NotificationPattern
{
    public class Notification
    {
        [JsonProperty("key")]
        public string Key { get; }

        [JsonProperty("message")]
        public string Message { get; }

        public Notification(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
