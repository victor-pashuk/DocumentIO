using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Domain.Models;
using DocumentIO.Infrastructure.Data;

namespace DocumentIO.Infrastructure.Repositories
{
    public class SharedLinkRepository : ISharedLinkRepository
    {
        private readonly DocumentDbContext _dbContext;

        public SharedLinkRepository(DocumentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SharedLink?> GetSharedLinkByIdAsync(long id)
        {
            return await _dbContext.SharingLinks.FindAsync(id);
        }

        public async Task AddSharedLinkAsync(SharedLink sharingLink)
        {
            await _dbContext.SharingLinks.AddAsync(sharingLink);
            await _dbContext.SaveChangesAsync();
        }
    }
}

