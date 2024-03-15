using Azure.Storage.Blobs;

namespace DataAccess.Factories
{
    /// <summary>
    /// Contains methods for constructing <see cref="BlobClientOptions"/> instances.
    /// </summary>
    public interface IBlobClientOptionsFactory
    {
        /// <summary>
        /// Creates <seealso cref="BlobClientOptions"/>.
        /// </summary>
        /// <param name="keyUri"></param>
        /// <returns></returns>
        BlobClientOptions Create(Uri keyUri);
    }
}
