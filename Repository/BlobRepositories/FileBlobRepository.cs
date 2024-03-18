using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DataAccess.Factories;
using DataAccess.Providers;
using Domain.BlobRepositories;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using BlobProperties = Azure.Storage.Blobs.Models.BlobProperties;

namespace DataAccess.BlobRepositories
{
    /// <summary>
    /// Responsible for uploading blob.
    /// </summary>
    public class FileBlobRepository : BaseBlobStorageRepository, IFileBlobRepository
    {
        private readonly string NameMetadataKey = "Name";
        private readonly string ContentTypeMetadataKey = "ContentType";
        private readonly string SizeInBytesMetadataKey = "SizeInBytes";

        public override string SubContainerName => "temp";

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBlobRepository"/> class.
        /// </summary>
        /// <param name="blobClientOptionsFactory"> A factory to create <see cref="BlobClientOptions"/>.</param>
        /// <param name="blobStorageConfigurationProvider">blob storage configuration provider.</param>
        /// <param name="logger">The logger <see cref="ILogger"/> for logging diagnostics information.</param>
        /// <param name="blobContainerFactory"></param>
        public FileBlobRepository(IBlobClientOptionsFactory blobClientOptionsFactory, 
            IBlobStorageConfigurationProvider blobStorageConfigurationProvider, 
            ILogger logger, 
            IBlobContainerFactory blobContainerFactory) 
            : base(blobClientOptionsFactory, blobStorageConfigurationProvider, logger, blobContainerFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="content"></param>
        /// <param name="resetStreamPosition"></param>
        /// <returns></returns>
        public async Task UploadAsync(FileModel file, Stream content, bool resetStreamPosition = true)
        {
            var blobClient = await GetBlobClientAsync(file.Id.ToString());

            if (resetStreamPosition)
            {
                ResetStreamPosition(content);
            }

            var blobHttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType };
            var metadata = new Dictionary<string, string>()
            {
                { NameMetadataKey, file.Name },
                { ContentTypeMetadataKey, file.ContentType },
                { SizeInBytesMetadataKey, file.SizeInBytes.ToString() }
            };
            var blobUploadOptions = new BlobUploadOptions
            {
                Metadata = metadata,
                HttpHeaders = blobHttpHeaders
            };

            await blobClient.UploadAsync(content, options: blobUploadOptions);
        }


        /// <summary>
        ///  Gets a read-only file content for the blob file.
        /// </summary>
        /// <param name="id">The path to the file to open.</param>
        /// <returns>The opened stream for reading from blob.</returns>
        public async Task<FileContentModel> OpenReadStreamAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var blobClient = await GetBlobClientAsync(id.ToString());

            try
            {
                var propertyResponse = await blobClient.GetPropertiesAsync();
                var stream = await blobClient.OpenReadAsync();
                var metadata = await GetFileMetadataAsync(id.ToString());

                var contentType = "application/octet-stream";
                var name = "default";
                if (metadata.TryGetValue(ContentTypeMetadataKey, out var originalContentType))
                {
                    contentType = originalContentType;
                }
                if (metadata.TryGetValue(NameMetadataKey, out var originalName))
                {
                    name = originalName;
                }

                return new FileContentModel
                {
                    FileStream = stream,
                    ContentType = contentType,
                    Name = name
                };
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                Logger.LogError(ex, "Failed to open, no file is found with given id '{id}'.", id);
                return null;
            }
        }

        private async Task<IDictionary<string, string>> GetFileMetadataAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

            var properties = await GetFilePropertiesAsync(filePath);
            return properties?.Metadata;
        }

        private async Task<BlobProperties> GetFilePropertiesAsync(string filePath)
        {
            var key = FormattableString.Invariant($"{SubContainerName}/{filePath}");
            var containerClient = await GetBlobClientAsync(filePath);
            var properties = await containerClient.GetPropertiesAsync();

            return properties;
        }

        private static void ResetStreamPosition(Stream content)
        {
            if (content.Position == 0) return;

            // Move to the start of the stream since other archives may have read from the stream 
            // and the position will be at the end.
            if (!content.CanSeek)
                throw new InvalidOperationException("Unable to upload the file. The stream position could not be set back to the beginning.");
            else
                content.Position = 0;
        }
    }
}
