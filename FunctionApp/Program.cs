using Domain.Settings;
using FunctionApp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

var azureFunctionsEnvironment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", optional: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices(services => {

        services.AddSingleton<ILoggerFactory>(serviceProvider => 
            LoggerFactory.Create(builder =>
             {
                 builder.AddConsole();
             }))
            .AddSingleton(serviceProvider =>
            {
                var factory = serviceProvider.GetService<ILoggerFactory>();
                return factory?.CreateLogger("Common");
            });

        services.BindSettings<BlobStorageSettings>("BlobStorage")
                .BindSettings<KeyVaultSettings>("KeyVault");
        services.AddFunctionAppModule(azureFunctionsEnvironment);
    })    
    .Build();

host.Run();