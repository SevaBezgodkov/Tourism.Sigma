using Domain.Events;
using RepositoryService.Background.Interfaces;
using RepositoryService.Handlers.Interfaces;

namespace RepositoryService.Background
{
    public class TasksHandler : ITasksHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public TasksHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private Dictionary<string, Type> eventsName = new Dictionary<string, Type>() {
            { UserAddedEvent.EventName, typeof(UserAddedEvent) },
            { UserUpdatedEvent.EventName, typeof(UserUpdatedEvent) }
        };

        public async Task InitializeStartTaskDictionary(string routingKey, string deserializedMessage)
        {
            if (eventsName.Keys.Contains(routingKey))
            {
                var eventType = eventsName[routingKey];

                var handlerType = typeof(ITaskHandler<>).MakeGenericType(eventType);

                var handlerInstance = _serviceProvider.GetService(handlerType);

                if (handlerInstance != null)
                {
                    var method = handlerType.GetMethod("HandleAsync");

                    var eventInstance = Activator.CreateInstance(eventType, deserializedMessage);

                    if (method != null && eventInstance != null)
                    {
                        var task = (Task)method.Invoke(handlerInstance, new[] { eventInstance });
                        await task;
                    }
                }
            }

        }
    }
}
