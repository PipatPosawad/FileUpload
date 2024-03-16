using DataAccess.BlobRepositories;
using DataAccess.Configurations;
using DataAccess.Factories;
using DataAccess.Providers;
using Domain.BlobRepositories;

namespace WebApi.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> that adds blob storage dependencies.
    /// </summary>
    public static class BlobStorageModule
    {
        /// <summary>
        /// Adds blob storage dependencies to the service collection.
        /// </summary>
        /// <param name="services">A services collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddBlobStorageModule(this IServiceCollection services)
        {
            services.AddSingleton<IBlobClientOptionsFactory, BlobClientOptionsFactory>();
            services.AddSingleton<IBlobContainerFactory, BlobContainerFactory>();
            services.AddBlobStorageDependencies();
            return services;
        }

        /// <summary>
        /// Adds blob storage dependencies to the service collection for local development.
        /// </summary>
        /// <param name="services">A services collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddLocalBlobStorageModule(this IServiceCollection services)
        {
            services.AddSingleton<IBlobClientOptionsFactory, LocalBlobClientOptionFactory>();
            services.AddSingleton<IBlobContainerFactory, LocalBlobContainerFactory>();
            services.AddBlobStorageDependencies();
            return services;
        }

        private static IServiceCollection AddBlobStorageDependencies(this IServiceCollection services)
        {
            services
                .AddSingleton<IStorageAccountConfiguration, StorageAccountConfiguration>()
                .AddSingleton<IKeyVaultConfiguration, KeyVaultConfiguration>()
                .AddScoped<IBlobStorageConfigurationProvider, BlobStorageConfigurationProvider>()
                .AddScoped<IFileBlobRepository, FileBlobRepository>();

            return services;
        }
    }
}
