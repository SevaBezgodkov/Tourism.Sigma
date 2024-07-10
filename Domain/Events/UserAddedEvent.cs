using Domain.Events.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Domain.Events
{
    public class UserAddedEvent : ITaskEvent<User>
    {
        public UserAddedEvent(string message)
        {
            var deserializedObject = JObject.Parse(message);

            Model = deserializedObject["Model"].ToObject<User>();
        }

        public User Model { get; set; }
        public static string EventName { get; set; } = "user.add";
    }
}
