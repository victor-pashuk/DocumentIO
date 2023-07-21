using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Services
{
    public interface IDocumentService
    {
        Task DeleteDocumentAsync(long documentId);
        Task<Document?> GetDocumentByIdAsync(long documentId);
        Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(long userId);
        Task IncrementDownloadCountAsync(long documentId);
        Task UpdateDocumentAsync(Document document);
        Task<long> CreateDocumentAsync(string name, string type, string fileGuid, long creatorId);
    }
}