using AuthUsers.Models.PublishModels.Interfaces;

namespace AuthUsers.Models.PublishModels
{
    public class UpdateUserPublishModel : IPublishModel<UserModel>
    {
        public UserModel? Model { get; set; }
        public string QueueName { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
