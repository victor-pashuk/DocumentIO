// Services/SharingLinkService.cs


using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Domain.Models;


namespace DocumentIO.Application.Services
{
    public class SharedLinkService : ISharedLinkService
    {
        private readonly ISharedLinkRepository _sharingLinkRepository;

        public SharedLinkService(ISharedLinkRepository sharingLinkRepository)
        {
            _sharingLinkRepository = sharingLinkRepository;
        }

        public async Task<SharedLink?> GetSharedLinkByIdAsync(long id)
        {
            return await _sharingLinkRepository.GetSharedLinkByIdAsync(id);
        }

        public async Task<bool> ValidateSharedLinkAsync(long id)
        {
            var sharingLink = await _sharingLinkRepository.GetSharedLinkByIdAsync(id);
            return sharingLink != null && sharingLink.ExpirationDateTime > DateTime.UtcNow;
        }

        public async Task<SharedLink> CreateSharedLinkAsync(long documentId, int expirationHours)
        {
            var expirationDateTime = DateTime.UtcNow.AddHours(expirationHours);

            var sharingLink = new SharedLink
            {
                ExpirationDateTime = expirationDateTime,
                DocumentId = documentId
            };

            await _sharingLinkRepository.AddSharedLinkAsync(sharingLink);
            return sharingLink;
        }
    }
}
