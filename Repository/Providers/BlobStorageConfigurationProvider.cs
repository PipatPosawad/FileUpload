using DataAccess.Configurations;
using Microsoft.WindowsAzure.Storage;

namespace DataAccess.Providers
{
    /// <summary>
    /// Blob storage configuration provider.
    /// </summary>
    public class BlobStorageConfigurationProvider : IBlobStorageConfigurationProvider
    {
        private readonly string ContainerName = "default";

        private readonly IStorageAccountConfiguration _storageAccountConfiguration;
        private readonly IKeyVaultConfiguration _keyVaultConfiguration;

        private BlobStorageConfiguration _blobStorageConfiguration;

        /// <summary>
        /// Initializes a new <see cref="BlobStorageConfigurationProvider"/> instance.
        /// </summary>
        /// <param name="storageAccountConfiguration"></param>
        /// <param name="keyVaultConfiguration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public BlobStorageConfigurationProvider(
            IStorageAccountConfiguration storageAccountConfiguration,
            IKeyVaultConfiguration keyVaultConfiguration)
        {
            _storageAccountConfiguration = storageAccountConfiguration
                ?? throw new ArgumentNullException(nameof(storageAccountConfiguration));

            _keyVaultConfiguration = keyVaultConfiguration
                ?? throw new ArgumentNullException(nameof(keyVaultConfiguration));
        }

        /// <summary>
        /// Gets the blob storage configuration. 
        /// </summary>
        /// <returns></returns>
        public BlobStorageConfiguration Get()
        {
            if (_blobStorageConfiguration != null)
            {
                return _blobStorageConfiguration;
            }

            if (!CloudStorageAccount.TryParse(_storageAccountConfiguration.ConnectionString, out var cloudStorageAccount))
                throw new Exception($"Unable to parse the connection string of storage account configuration.");

            _blobStorageConfiguration = new BlobStorageConfiguration(
                blobEndpointUri: cloudStorageAccount.BlobEndpoint,
                containerName: ContainerName,
                keyUri: _keyVaultConfiguration.Uri);

            return _blobStorageConfiguration;
        }
    }
}
