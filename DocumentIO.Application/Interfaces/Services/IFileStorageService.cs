namespace DocumentIO.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(string fileName, byte[] fileData);
        Task<byte[]> DownloadFileAsync(string fileGuid);
        Task DeleteFileAsync(string fileGuid);
    }
}

