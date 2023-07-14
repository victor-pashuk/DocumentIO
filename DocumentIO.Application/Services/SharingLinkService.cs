// Services/SharingLinkService.cs


using DocumentIO.Application.Interfaces.Repositories;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Services
{
    public class SharingLinkService : ISharingLinkService
    {
        private readonly ISharingLinkRepository _sharingLinkRepository;

        public SharingLinkService(ISharingLinkRepository sharingLinkRepository)
        {
            _sharingLinkRepository = sharingLinkRepository;
        }

        public async Task<bool> ValidateSharingLinkAsync(string token)
        {
            var sharingLink = await _sharingLinkRepository.GetSharingLinkByTokenAsync(token);
            return sharingLink != null && sharingLink.ExpirationDateTime > DateTime.UtcNow;
        }

        public async Task<SharingLink> CreateSharingLinkAsync(long documentId, int expirationHours)
        {
            var token = GenerateToken();
            var expirationDateTime = DateTime.UtcNow.AddHours(expirationHours);

            var sharingLink = new SharingLink
            {
                Token = token,
                ExpirationDateTime = expirationDateTime,
                DocumentId = documentId
            };

            await _sharingLinkRepository.AddSharingLinkAsync(sharingLink);
            return sharingLink;
        }

        private string GenerateToken()
        {
            return string.Empty;
            // Implement token generation logic
            // Example:
            // var token = some_token_generation_logic_here;
            // return token;
        }
    }
}
