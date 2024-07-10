namespace AuthUsers.Models.PublishModels.Interfaces
{
    public interface IPublishModel<T>
    {
        public T? Model { get; set; }
        public string QueueName { get; set; }
    }
}
