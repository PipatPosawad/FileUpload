namespace Domain.Settings
{
    /// <summary>
    /// Contains blob storage settings
    /// </summary>
    public class BlobStorageSettings
    {
        /// <summary>
        /// Gets or sets the flag whether to disable encryption on local development.
        /// </summary>
        public bool DisableEncryptionOnLocal { get; init; }
    }
}
