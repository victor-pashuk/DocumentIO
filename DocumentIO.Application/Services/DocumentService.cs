
using System.IO;
using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Domain.Models;
using ShortUrl;

namespace DocumentIO.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserDocumentsRepository _userDocumentsRepository;

        public DocumentService(IDocumentRepository documentRepository, IUserDocumentsRepository userDocumentsRepository)
        {
            _documentRepository = documentRepository;
            _userDocumentsRepository = userDocumentsRepository;
        }

        public async Task<Document?> GetDocumentByIdAsync(long documentId)
        {
            return await _documentRepository.GetDocumentByIdAsync(documentId);
        }

        public async Task<long> CreateDocumentAsync(string name, string type, string fileGuid, long creatorId)
        {
            var document = new Document(name, type, fileGuid, creatorId);
            var documentId = await _documentRepository.CreateDocumentAsync(document);

            var userDocuments = await _userDocumentsRepository.GetUserDocumentsByUserIdAsync(creatorId);
            if (userDocuments != null)
            {
                await _userDocumentsRepository.AddDocumentToUserAsync(userDocuments, document);
            }
            else
            {
                userDocuments = new UserDocuments
                {
                    UserId = creatorId,
                    Documents = new List<Document> { document }
                };
                await _userDocumentsRepository.CreateUserDocumentsAsync(userDocuments);
            }

            return documentId;
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            await _documentRepository.UpdateDocumentAsync(document);
        }

        public async Task DeleteDocumentAsync(long documentId)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(documentId);
            if (document != null)
            {
                var userDocuments = await _userDocumentsRepository.GetUserDocumentsByUserIdAsync(document.CreatorId);
                if (userDocuments != null)
                {
                    await _userDocumentsRepository.RemoveDocumentFromUserAsync(userDocuments, document);
                }

                await _documentRepository.DeleteDocumentAsync(document);
            }
        }

        public async Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(long userId)
        {
            var userDocuments = await _userDocumentsRepository.GetUserDocumentsByUserIdAsync(userId);
            return userDocuments?.Documents ?? Enumerable.Empty<Document>();
        }

        public async Task IncrementDownloadCountAsync(long documentId)
        {
            await _documentRepository.IncrementDownloadCountAsync(documentId);
        }

    }
}
