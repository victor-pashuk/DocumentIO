
using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Domain.Models;
using DocumentIO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentIO.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DocumentDbContext _dbContext;

        public UserRepository(DocumentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
