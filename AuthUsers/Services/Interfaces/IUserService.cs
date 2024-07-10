using AuthUsers.Models;
using Domain.Models;

namespace AuthUsers.Services.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(UserModel model);
        Task UpdateByIdAsync(Guid userId, UserModel model);
        Task<UserModel> GetUserById(Guid userId);
    }
}
