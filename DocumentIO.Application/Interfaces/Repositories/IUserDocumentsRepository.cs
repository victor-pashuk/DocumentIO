
using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Repositories
{
    public interface IUserDocumentsRepository
    {
        Task AddDocumentToUserAsync(UserDocuments userDocuments, Document document);
        Task<UserDocuments> CreateUserDocumentsAsync(UserDocuments userDocuments);
        Task<UserDocuments?> GetUserDocumentsByUserIdAsync(long userId);
        Task RemoveDocumentFromUserAsync(UserDocuments userDocuments, Document document);
    }
}