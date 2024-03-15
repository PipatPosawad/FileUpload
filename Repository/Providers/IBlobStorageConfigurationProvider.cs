using DataAccess.Configurations;

namespace DataAccess.Providers
{
    public interface IBlobStorageConfigurationProvider
    {
        /// <summary>
        /// Gets the blob storage configuration. 
        /// </summary>
        /// <returns>A blob storage configuration.</returns>
        BlobStorageConfiguration Get();
    }
}
