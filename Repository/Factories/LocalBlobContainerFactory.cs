using Azure.Storage.Blobs;
using Azure.Storage;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Factories
{
    /// <summary>
    ///  A factory for <see cref="BlobContainerClient"/>
    /// </summary>
    public class LocalBlobContainerFactory : IBlobContainerFactory
    {
        private readonly StorageSharedKeyCredential _credential;

        /// <summary>
        /// Initializes a <see cref="LocalBlobContainerFactory"/>
        /// </summary>
        /// <param name="configuration"></param>
        public LocalBlobContainerFactory(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SystemStorage");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Missing SystemStorage in the configuration file");
            }

            // Parse the creds from the connection string in a form of "damageType=Magic;damage=230;armor=7"
            var getValue = (string[] items, string key) => items.First(x => x.StartsWith(key)).Remove(0, key.Length + 1);
            var items = connectionString
                .Split(";");
            var accountName = getValue(items, "AccountName");
            var accountKey = getValue(items, "AccountKey");

            _credential = new StorageSharedKeyCredential(accountName, accountKey);
        }

        /// <summary>
        /// Gets an instance of <see cref="BlobContainerClient"/>
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public BlobContainerClient Get(Uri uri, BlobClientOptions options)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return new BlobContainerClient(uri, _credential, options);
        }
    }
}
