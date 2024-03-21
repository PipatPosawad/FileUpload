using DataAccess.BlobRepositories;
using DataAccess.Configurations;
using DataAccess.Factories;
using DataAccess.Providers;
using Domain.BlobRepositories;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp
{
    /// <summary>
    /// Extensions for function applications
    /// </summary>
    public static class FunctionsModule
    {
        /// <summary>
        /// Adds modules for the function app
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddFunctionAppModule(this IServiceCollection services, string azureFunctionsEnvironment)
        {
            if (azureFunctionsEnvironment == "Development")
            {
                services.AddSingleton<IBlobClientOptionsFactory, LocalBlobClientOptionFactory>()
                        .AddSingleton<IBlobContainerFactory, LocalBlobContainerFactory>();
            }
            else
            {
                services.AddSingleton<IBlobClientOptionsFactory, BlobClientOptionsFactory>()
                        .AddSingleton<IBlobContainerFactory, BlobContainerFactory>();
            }

            services
                .AddSingleton<IStorageAccountConfiguration, StorageAccountConfiguration>()
                .AddSingleton<IKeyVaultConfiguration, KeyVaultConfiguration>()
                .AddScoped<IBlobStorageConfigurationProvider, BlobStorageConfigurationProvider>()
                .AddScoped<IFileBlobRepository, FileBlobRepository>();

            services.AddScoped<IMailNotificationService, MailNotificationService>();

            return services;
        }
    }
}
