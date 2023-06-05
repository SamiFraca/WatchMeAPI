using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using WatchMe.Models;
using Microsoft.AspNetCore.Mvc;
using WatchMe.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

public class AzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;
    private readonly string _connectionString;
    private readonly DataContext _dbContext;

    public AzureBlobStorageService(IConfiguration configuration, DataContext dbContext)
    {
        _connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];
        _blobServiceClient = new BlobServiceClient(_connectionString);
        _dbContext = dbContext;
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

    public async Task<Bar> UpdateImageAsync(string fileName, Stream imageStream, int id)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(
            _containerName
        );
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(imageStream, overwrite: true);
        var actualBar = await _dbContext.Bars.FirstOrDefaultAsync(bar => bar.Id == id);
        if (actualBar != null)
        {
            if (blobClient.Uri.ToString() != null)
            {
                actualBar.ImageUrl = blobClient.Uri.ToString();
                 await _dbContext.SaveChangesAsync();
            }
        }else{
            throw new ArgumentException("Invalid ID provided.");
        }
        return actualBar;
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
