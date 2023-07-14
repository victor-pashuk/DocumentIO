// Repositories/DocumentRepository.cs

using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Domain.Models;
using DocumentIO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentIO.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentDbContext _dbContext;

        public DocumentRepository(DocumentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Document> GetDocumentByIdAsync(long documentId)
        {
            return await _dbContext.Documents.FindAsync(documentId);
        }

        public async Task<long> CreateDocumentAsync(Document document)
        {
            await _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();
            return document.Id;
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            _dbContext.Documents.Update(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(Document document)
        {
            _dbContext.Documents.Remove(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task IncrementDownloadCountAsync(long documentId)
        {
            var document = await _dbContext.Documents.FindAsync(documentId);
            if (document != null)
            {
                document.DownloadCount++;
                await _dbContext.SaveChangesAsync();
            }

        }

    }
}
