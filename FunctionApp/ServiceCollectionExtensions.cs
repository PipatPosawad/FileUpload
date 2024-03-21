using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp
{
    /// <summary>
    /// Contains extension methods for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Binds a specific <see cref="IConfiguration"/> section from the <paramref name="sectionName"/> to a <typeparamref name="TSettings"/>
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="services"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static IServiceCollection BindSettings<TSettings>(this IServiceCollection services, string sectionName) where TSettings : class
        {
            services
                .AddOptions<TSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(sectionName).Bind(settings);
                });

            return services;
        }
    }
}
