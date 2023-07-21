
using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(long userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(User user);
    }
}