using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DocumentIO.Application.Interfaces.Services;
using DocumentIO.Application.Utility;

namespace DocumentIO.Infrastructure.Services
{
    public class AzureFileStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureFileStorageService(string connectionString, string containerName)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerName = containerName;
        }

        public async Task<string> UploadFileAsync(string fileName, byte[] fileData)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();
            var fileGuid = Guid.NewGuid().ToString();
            var blobClient = containerClient.GetBlobClient(fileGuid);
            await using (var memoryStream = new MemoryStream(fileData))
            {
                await blobClient.UploadAsync(memoryStream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = ContentTypeHelper.GetContentType(fileName) } });
            }

            return fileGuid;
        }

        public async Task<byte[]> DownloadFileAsync(string fileGuid)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(fileGuid);
            var response = await blobClient.DownloadAsync();

            using var memoryStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task DeleteFileAsync(string fileGuid)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileGuid);
            await blobClient.DeleteAsync();
        }


    }
}
