using DocumentIO.Domain.Models;

namespace DocumentIO.Application.Interfaces.Repositories
{
    public interface ISharedLinkRepository
    {
        Task AddSharedLinkAsync(SharedLink sharingLink);
        Task<SharedLink?> GetSharedLinkByIdAsync(long id);
    }
}