using AuthUsers.Models;
using AuthUsers.Services.Interfaces;
using Domain.Models;
using Domain.Services.Interfaces;

namespace AuthUsers.Services
{
    public class UserService : IUserService
    {
        private readonly IPublisherServiceBus _serviceBus;

        public UserService(IPublisherServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }
        public async Task AddAsync(UserModel model)
        {
            var ignoreFields = model.IgnoreRabbitFieldsModel;
            ignoreFields.QueueName = "userQueue";
            ignoreFields.ReceiverModelType = typeof(User);

            _serviceBus.PublishMessage(model, "userExchange", "userQueue", "user.add");
        }

        public Task<UserModel> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
