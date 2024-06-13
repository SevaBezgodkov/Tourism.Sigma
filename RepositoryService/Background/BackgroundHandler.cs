using Domain.Models;
using Newtonsoft.Json;
using RepositoryService.Background.Interfaces;
using RepositoryService.Command.Interfaces;

namespace RepositoryService.Background
{
    public class BackgroundHandler : IBackgroundHandler
    {
        private readonly ICommandUserRepository _commandUserRepository;
        public BackgroundHandler(ICommandUserRepository commandUserRepository) 
        {
            _commandUserRepository = commandUserRepository;
        }

        public async Task HandleMessageByRoutingKey(string routingKey, string receivedMessageFromQueue)
        {
            switch (routingKey)
            {
                case "user.add":
                    var userMessage = JsonConvert.DeserializeObject<User>(receivedMessageFromQueue);
                    await _commandUserRepository.AddAsync(userMessage);
                    break;
            }
        }
    }
}
