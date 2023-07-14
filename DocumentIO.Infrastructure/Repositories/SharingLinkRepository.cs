using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Domain.Models;
using DocumentIO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentIO.Infrastructure.Repositories
{
    public class SharingLinkRepository : ISharingLinkRepository
    {
        private readonly DocumentDbContext _dbContext;

        public SharingLinkRepository(DocumentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SharingLink> GetSharingLinkByTokenAsync(string token)
        {
            return await _dbContext.SharingLinks.FirstOrDefaultAsync(sl => sl.Token == token);
        }

        public async Task AddSharingLinkAsync(SharingLink sharingLink)
        {
            await _dbContext.SharingLinks.AddAsync(sharingLink);
            await _dbContext.SaveChangesAsync();
        }
    }
}

