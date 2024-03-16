using Azure.Identity;
using Azure.Storage.Blobs;

namespace DataAccess.Factories
{
    /// <summary>
    ///  A factory for <see cref="BlobContainerClient"/>
    /// </summary>
    public class BlobContainerFactory : IBlobContainerFactory
    {
        /// <summary>
        /// Gets an instance of <see cref="BlobContainerClient"/>
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public BlobContainerClient Get(Uri uri, BlobClientOptions options)
        {
            ArgumentNullException.ThrowIfNull(uri);
            ArgumentNullException.ThrowIfNull(options);

            return new BlobContainerClient(uri, new DefaultAzureCredential(), options);
        }
    }
}
