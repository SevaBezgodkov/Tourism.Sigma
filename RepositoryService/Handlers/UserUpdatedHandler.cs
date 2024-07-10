using Domain.Events;
using RepositoryService.Command.Interfaces;
using RepositoryService.Handlers.Interfaces;

namespace RepositoryService.Handlers
{
    public class UserUpdatedHandler : ITaskHandler<UserUpdatedEvent>
    {
        private readonly ICommandUserRepository _userRepo;
        public UserUpdatedHandler(ICommandUserRepository userRepo) 
        {
            _userRepo = userRepo;
        }

        public async Task HandleAsync(UserUpdatedEvent taskEvent)
        {
            await _userRepo.UpdateAsync(new Guid(taskEvent.UserId), taskEvent.Model);
        }
    }
}
