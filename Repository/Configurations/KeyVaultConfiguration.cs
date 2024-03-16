using Domain.Settings;
using Microsoft.Extensions.Options;

namespace DataAccess.Configurations
{
    /// <summary>
    /// The KeyVault configuration.
    /// </summary>
    public class KeyVaultConfiguration : IKeyVaultConfiguration
    {
        /// <summary>
        /// Gets the Uri.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Initializes a new <see cref="KeyVaultConfiguration"/> instance.
        /// </summary>
        /// <param name="keyVaultSettings">the KeyVault settings.</param>
        public KeyVaultConfiguration(IOptions<KeyVaultSettings> keyVaultSettings)
        {
            if (keyVaultSettings == null)
                throw new ArgumentNullException(nameof(keyVaultSettings));

            var uriString = keyVaultSettings.Value?.Uri;
            if (string.IsNullOrWhiteSpace(uriString))
                throw new ArgumentException("KeyVault Uri cannot be null or white space.", nameof(keyVaultSettings));

            Uri = new Uri(uriString);
        }
    }
}
