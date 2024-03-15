using Azure.Storage.Blobs;
using DataAccess.Factories;
using DataAccess.Providers;
using Microsoft.Extensions.Logging;

namespace DataAccess.BlobRepositories
{
    /// <summary>
    /// Base class for the blob storage repositories.
    /// </summary>
    public abstract class BaseBlobStorageRepository
    {
        private readonly IBlobClientOptionsFactory _blobClientOptionsFactory;
        private readonly IBlobContainerFactory _blobContainerFactory;

        /// <summary>
        /// The sub container name
        /// </summary>
        public abstract string SubContainerName { get; }

        /// <summary>
        /// The blob storage configuration provider.
        /// </summary>
        protected IBlobStorageConfigurationProvider BlobStorageConfigurationProvider { get; }

        /// <summary>
        /// The logger for logging diagnostics information.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Initializes the base storage repository.
        /// </summary>
        /// <param name="blobClientOptionsFactory">A factory to create configuration for a blob client</param>
        /// <param name="blobStorageConfigurationProvider">blob storage configuration provider.</param>
        /// <param name="logger">The logger <see cref="ILogger"/> for logging diagnostics information.</param>
        /// <param name="blobContainerFactory">An factory to create a blob container</param>
        protected BaseBlobStorageRepository(
            IBlobClientOptionsFactory blobClientOptionsFactory,
            IBlobStorageConfigurationProvider blobStorageConfigurationProvider,
            ILogger logger,
            IBlobContainerFactory blobContainerFactory)
        {
            _blobClientOptionsFactory = blobClientOptionsFactory ?? throw new ArgumentNullException(nameof(blobClientOptionsFactory));
            BlobStorageConfigurationProvider = blobStorageConfigurationProvider ?? throw new ArgumentNullException(nameof(blobStorageConfigurationProvider));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _blobContainerFactory = blobContainerFactory ?? throw new ArgumentNullException(nameof(blobContainerFactory));
        }

        protected async Task<BlobContainerClient> CreateBlobContainerClientAsync()
        {
            var blobStorageConfiguration = BlobStorageConfigurationProvider.Get();
            var options = _blobClientOptionsFactory.Create(blobStorageConfiguration.KeyUri);
            var containerClient = _blobContainerFactory.Get(
                blobStorageConfiguration.ContainerUri,
                options);

            if (!await containerClient.ExistsAsync()) 
                await containerClient.CreateAsync();

            return containerClient;
        }

        protected async Task<BlobClient> GetBlobClientAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

            var targetFilePath = FormattableString.Invariant($"{SubContainerName}/{filePath}");

            var containerClient = await CreateBlobContainerClientAsync();
            var blobClient = containerClient.GetBlobClient(targetFilePath);

            return blobClient;
        }
    }
}
