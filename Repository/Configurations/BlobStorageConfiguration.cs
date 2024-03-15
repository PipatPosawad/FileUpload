namespace DataAccess.Configurations
{
    /// <summary>
    /// Contains the blob storage configuration.
    /// </summary>
    public class BlobStorageConfiguration
    {
        /// <summary>
        /// Gets the container URI.
        /// </summary>
        public Uri ContainerUri { get; }

        /// <summary>
        /// Gets the container name
        /// </summary>
        public string ContainerName { get; }

        /// <summary>
        /// Gets the key URI.
        /// </summary>
        public Uri KeyUri { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobStorageConfiguration"/> class.
        /// </summary>
        /// <param name="blobEndpointUri"></param>
        /// <param name="containerName"></param>
        /// <param name="keyUri"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public BlobStorageConfiguration(
            Uri blobEndpointUri,
            string containerName,
            Uri keyUri)
        {
            ArgumentNullException.ThrowIfNull(blobEndpointUri);

            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(containerName));
            }

            KeyUri = keyUri ?? throw new ArgumentNullException(nameof(keyUri));

            var blobEndpoint = blobEndpointUri.AbsoluteUri.TrimEnd('/');
            ContainerUri = new Uri($"{blobEndpoint}/{containerName}");
            ContainerName = containerName;
        }
    }
}
