using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Services
{
    public interface IDocumentService
    {
        Task DeleteDocumentAsync(int documentId);
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(long userId);
        Task IncrementDownloadCountAsync(int documentId);
        Task UpdateDocumentAsync(Document document);
        Task<long> UploadDocumentAsync(string name, string type, byte[] data, long creatorId);
    }
}