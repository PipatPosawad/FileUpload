using Domain.Services;
using Service;

namespace WebApi.DependencyInjection
{
    /// <summary>
    /// Contains extension methods to <see cref="IServiceCollection"/> that adds API dependencies.
    /// </summary>
    public static class ApiModule
    {
        /// <summary>
        /// Adds API dependencies to the service collection.
        /// </summary>
        /// <param name="services">A services collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services.AddScoped<IFileUploadService, FileUploadService>();

            return services;
        }
    }
}
