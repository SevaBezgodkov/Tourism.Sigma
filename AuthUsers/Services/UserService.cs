using AuthUsers.Models;
using AuthUsers.Models.PublishModels;
using AuthUsers.Services.Interfaces;
using AutoMapper;
using Domain.Events;
using Domain.Models;
using Domain.Services.Interfaces;

namespace AuthUsers.Services
{
    public class UserService : IUserService
    {
        private readonly IPublisherServiceBus _serviceBus;
        private readonly IMapper _mapper;
        public UserService(IPublisherServiceBus serviceBus, IMapper mapper)
        {
            _serviceBus = serviceBus;
            _mapper = mapper;
        }
        public async Task AddAsync(UserModel model)
        {

            var addUserPublishModel = new AddUserPublishModel
            {
                Model = model,
                QueueName = "userQueue"
            };

            _serviceBus.PublishMessage(addUserPublishModel, "userExchange", "userQueue", "user.add");
        }

        public async Task UpdateByIdAsync(Guid userId, UserModel model)
        {
            var updateUserPublishModel = new UpdateUserPublishModel
            {
                Model = model,
                QueueName = "userQueue",
                UserId = userId,
            };

            _serviceBus.PublishMessage(updateUserPublishModel, "userExchange", "userQueue", "user.update");
        }

        public Task<UserModel> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
