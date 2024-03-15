namespace DataAccess.Configurations
{
    /// <summary>
    /// Interface of the storage account configuration.
    /// </summary>
    public interface IStorageAccountConfiguration
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        string ConnectionString { get; }
    }
}
