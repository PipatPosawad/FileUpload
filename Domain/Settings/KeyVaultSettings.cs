namespace Domain.Settings
{
    /// <summary>
    /// Settings about KeyVault
    /// </summary>
    public class KeyVaultSettings
    {
#pragma warning disable CA1056 // Uri properties should not be strings
        /// <summary>
        /// Gets and sets a URI of Key vault.
        /// </summary>
        public string Uri { get; init; }
#pragma warning restore CA1056 // Uri properties should not be strings
    }
}
