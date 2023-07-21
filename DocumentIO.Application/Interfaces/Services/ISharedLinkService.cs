using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Services
{
    public interface ISharedLinkService
    {
        Task<SharedLink?> GetSharedLinkByIdAsync(long id);
        Task<bool> ValidateSharedLinkAsync(long id);
        Task<SharedLink> CreateSharedLinkAsync(long documentId, int expirationHours);
    }
}
