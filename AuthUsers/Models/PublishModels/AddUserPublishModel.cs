using AuthUsers.Models.PublishModels.Interfaces;

namespace AuthUsers.Models.PublishModels
{
    public class AddUserPublishModel : IPublishModel<UserModel>
    {
        public UserModel? Model { get; set; }
        public string QueueName { get; set; } = null!;
    }
}
