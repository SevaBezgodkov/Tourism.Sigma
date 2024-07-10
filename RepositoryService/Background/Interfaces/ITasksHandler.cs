namespace RepositoryService.Background.Interfaces
{
    public interface ITasksHandler
    {
        Task InitializeStartTaskDictionary(string routingKey, string deserializedMessage);
    }
}
