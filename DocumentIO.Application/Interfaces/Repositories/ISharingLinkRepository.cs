using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Repositories
{
    public interface ISharingLinkRepository
    {
        Task AddSharingLinkAsync(SharingLink sharingLink);
        Task<SharingLink> GetSharingLinkByTokenAsync(string token);
    }
}