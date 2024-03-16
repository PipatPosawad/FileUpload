using Azure.Identity;
using Azure.Security.KeyVault.Keys.Cryptography;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Domain;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace DataAccess.Factories
{
    /// <summary>
    /// This is for local developement.
    /// Contains methods for constructing <see cref="BlobClientOptions"/> instances.
    /// </summary>
    public class LocalBlobClientOptionFactory : IBlobClientOptionsFactory
    {
        /// <summary>
        /// Gets the flag whether to disable encryption on local development.
        /// </summary>
        public bool DisableEncryptionOnLocal { get; }

        /// <summary>
        /// Initializes a new <see cref="LocalBlobClientOptionFactory"/> instance.
        /// </summary>
        /// <param name="blobStorageSettings">the BlobStorage settings.</param>
        public LocalBlobClientOptionFactory(IOptions<BlobStorageSettings> blobStorageSettings)
        {
            if (blobStorageSettings?.Value == null)
            {
                throw new ArgumentNullException(nameof(blobStorageSettings));
            }

            DisableEncryptionOnLocal = blobStorageSettings.Value.DisableEncryptionOnLocal;
        }

        /// <summary>
        /// Creates <seealso cref="BlobClientOptions"/>.
        /// </summary>
        /// <param name="keyUri"></param>
        /// <returns></returns>
        public BlobClientOptions Create(Uri keyUri)
        {
            if (keyUri == null)
                throw new ArgumentNullException(nameof(keyUri));

            if (DisableEncryptionOnLocal)
                return new BlobClientOptions();

            var cred = new DefaultAzureCredential();
            var jwk = KeyUtilities.CreateRsaKey(includePrivateParameters: true);
            var cryptoClient = new CryptographyClient(jwk);
            var keyResolver = new KeyResolver(cred);
            var encryptionOptions = new ClientSideEncryptionOptions(ClientSideEncryptionVersion.V2_0)
            {
                KeyEncryptionKey = cryptoClient,
                KeyResolver = keyResolver,
                KeyWrapAlgorithm = "RSA-OAEP"
            };

            return new SpecializedBlobClientOptions(BlobClientOptions.ServiceVersion.V2019_02_02)
            {
                ClientSideEncryption = encryptionOptions
            };
        }
    }

}
