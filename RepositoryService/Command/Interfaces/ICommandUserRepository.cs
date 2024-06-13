using Domain.Models;

namespace RepositoryService.Command.Interfaces
{
    public interface ICommandUserRepository
    {
        Task AddAsync(User model);
    }
}
