namespace AuthUsers.Models.PublishModels
{
    public class UserPublishModel
    {
        public UserModel? UserModel { get; set; }
        public string queueName { get; set; }
        public string Type { get; set; }
    }
}
