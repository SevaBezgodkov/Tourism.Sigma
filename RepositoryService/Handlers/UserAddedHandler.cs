using Domain.Events;
using Domain.Events.Interfaces;
using Domain.Models;
using RepositoryService.Command;
using RepositoryService.Command.Interfaces;
using RepositoryService.Handlers.Interfaces;

namespace RepositoryService.Handlers
{
    public class UserAddedHandler : ITaskHandler<UserAddedEvent>
    {
        private readonly ICommandUserRepository _userRepo;
        public UserAddedHandler(ICommandUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task HandleAsync(UserAddedEvent taskEvent)
        {
            await _userRepo.AddAsync(taskEvent.Model);
        }
    }
}
