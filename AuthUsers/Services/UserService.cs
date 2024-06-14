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
            //model.IgnoreRabbitFieldsModel = new RabbitFieldsModel
            //{
            //    QueueName = "userQueue"
            //};
            model.QueueName = "userQueue";
            _serviceBus.PublishMessage(model, "userExchange", "userQueue", "user.add");
        }

        public Task<UserModel> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
