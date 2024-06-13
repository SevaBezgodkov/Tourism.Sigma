namespace RepositoryService.Background.Interfaces
{
    public interface IBackgroundHandler
    {
        Task HandleMessageByRoutingKey(string routingKey, string receivedMessageFromQueue);
    }
}
