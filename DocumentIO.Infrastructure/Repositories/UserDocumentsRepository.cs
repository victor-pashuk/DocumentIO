
using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Domain.Models;
using DocumentIO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentIO.Infrastructure.Repositories
{
    public class UserDocumentsRepository : IUserDocumentsRepository
    {
        private readonly DocumentDbContext _dbContext;

        public UserDocumentsRepository(DocumentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDocuments> GetUserDocumentsByUserIdAsync(int userId)
        {
            return await _dbContext.UserDocuments
                .Include(ud => ud.Documents)
                .FirstOrDefaultAsync(ud => ud.UserId == userId);
        }

        public async Task AddDocumentToUserAsync(UserDocuments userDocuments, Document document)
        {
            userDocuments.Documents.Add(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveDocumentFromUserAsync(UserDocuments userDocuments, Document document)
        {
            userDocuments.Documents.Remove(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDocuments> CreateUserDocumentsAsync(UserDocuments userDocuments)
        {
            await _dbContext.UserDocuments.AddAsync(userDocuments);
            await _dbContext.SaveChangesAsync();
            return userDocuments;
        }
    }
}
