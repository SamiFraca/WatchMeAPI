using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

public class AzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;
    private readonly string _connectionString;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];
        _blobServiceClient = new BlobServiceClient(_connectionString);
    }

    public async Task<string> UploadImageAsync(string fileName, Stream imageStream)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(
            _containerName
        );
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(imageStream, overwrite: true);

        return blobClient.Uri.ToString();
    }

    public async Task<Stream> GetImageAsync(string fileName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(
            _containerName
        );
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        BlobDownloadInfo downloadInfo = await blobClient.DownloadAsync();
        return downloadInfo.Content;
    }
}
