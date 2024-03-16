using Microsoft.Extensions.Configuration;

namespace DataAccess.Configurations
{
    public class StorageAccountConfiguration : IStorageAccountConfiguration
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Initializes a new <see cref="StorageAccountConfiguration"/> instance.
        /// </summary>
        /// <param name="configuration">Configuration properties.</param>
        public StorageAccountConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("SystemStorage");
        }

        /// <summary>
        /// Initializes a new <see cref="StorageAccountConfiguration"/> instance.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public StorageAccountConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
