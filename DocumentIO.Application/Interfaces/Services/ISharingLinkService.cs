using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Services
{
    public interface ISharingLinkService
    {
        Task<bool> ValidateSharingLinkAsync(string token);
        Task<SharingLink> CreateSharingLinkAsync(long documentId, int expirationHours);
    }
}
