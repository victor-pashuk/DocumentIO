
using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Repositories
{
    public interface IDocumentRepository
    {
        Task<long> CreateDocumentAsync(Document document);
        Task DeleteDocumentAsync(Document document);
        Task<Document> GetDocumentByIdAsync(long documentId);
        Task IncrementDownloadCountAsync(long documentId);
        Task UpdateDocumentAsync(Document document);
    }
}