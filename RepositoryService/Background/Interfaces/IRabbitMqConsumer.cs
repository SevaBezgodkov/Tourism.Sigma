namespace RepositoryService.Background.Interfaces
{
    public interface IRabbitMqConsumer
    {
        void ReceiveMessages();
    }
}
