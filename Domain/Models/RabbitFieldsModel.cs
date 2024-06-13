
namespace Domain.Models
{
    public class RabbitFieldsModel
    {
        public string QueueName { get; set; } = null!;
        public Type ReceiverModelType { get; set; } = null!;
    }
}
