using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(long userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(User user);
    }
}