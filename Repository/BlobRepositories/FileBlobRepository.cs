using Azure.Storage.Blobs;
using DataAccess.Factories;
using DataAccess.Providers;
using Domain.BlobRepositories;
using Microsoft.Extensions.Logging;

namespace DataAccess.BlobRepositories
{
    /// <summary>
    /// Responsible for uploading blob.
    /// </summary>
    public class FileBlobRepository : BaseBlobStorageRepository, IFileBlobRepository
    {
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
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="resetStreamPosition"></param>
        /// <returns></returns>
        public async Task UploadAsync(string filePath, Stream content, bool resetStreamPosition = true)
        {
            var blobClient = await GetBlobClientAsync(filePath);

            if (resetStreamPosition)
            {
                ResetStreamPosition(content);
            }

            await blobClient.UploadAsync(content);
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
