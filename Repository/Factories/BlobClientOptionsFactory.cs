using Azure.Identity;
using Azure.Security.KeyVault.Keys.Cryptography;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace DataAccess.Factories
{
    /// <summary>
    /// Contains methods for constructing <see cref="BlobClientOptions"/> instances.
    /// </summary>
    public class BlobClientOptionsFactory : IBlobClientOptionsFactory
    {
        /// <summary>
        /// Creates <seealso cref="BlobClientOptions"/>.
        /// </summary>
        /// <param name="keyUri"></param>
        /// <returns></returns>
        public BlobClientOptions Create(Uri keyUri)
        {
            if (keyUri == null)
                throw new ArgumentNullException(nameof(keyUri));

            var cred = new DefaultAzureCredential();
            var cryptoClient = new CryptographyClient(keyUri, cred);
            var keyResolver = new KeyResolver(cred);
            var encryptionOptions = new ClientSideEncryptionOptions(ClientSideEncryptionVersion.V2_0)
            {
                KeyEncryptionKey = cryptoClient,
                KeyResolver = keyResolver,
                KeyWrapAlgorithm = "RSA-OAEP"
            };

            return new SpecializedBlobClientOptions() { ClientSideEncryption = encryptionOptions };
        }
    }
}
