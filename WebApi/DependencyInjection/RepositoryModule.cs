using Domain.BlobRepositories;
using DataAccess.BlobRepositories;
using DataAccess.Factories;

namespace WebApi.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> that adds repository dependencies.
    /// </summary>
    public static class RepositoryModule
    {
        /// <summary>
        /// Adds repositories' dependencies to the service collection.
        /// </summary>
        /// <param name="services">A services collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositoryModule(this IServiceCollection services)
        {
            services
                .AddSingleton<IBlobClientOptionsFactory, BlobClientOptionsFactory>()
                .AddScoped<IFileBlobRepository, FileBlobRepository>();

            return services;
        }
    }
}
