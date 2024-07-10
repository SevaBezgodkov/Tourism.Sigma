using Domain.Events.Interfaces;

namespace RepositoryService.Handlers.Interfaces
{
    public interface ITaskHandler<T>
    {
        Task HandleAsync(T taskEvent);
    }
}
