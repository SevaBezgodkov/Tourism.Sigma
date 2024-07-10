using Domain.Events.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Domain.Events
{
    public class UserUpdatedEvent : ITaskEvent<User>
    {
        public UserUpdatedEvent(string message)
        {
            var deserializedObject = JObject.Parse(message);

            Model = deserializedObject["Model"].ToObject<User>();
            UserId = deserializedObject["UserId"]?.ToString();
        }

        public User Model { get; set; }
        public static string EventName { get; set; } = "user.update";
        public string UserId { get; set; }
    }
}
