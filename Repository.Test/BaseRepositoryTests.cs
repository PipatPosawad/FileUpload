using Azure.Identity;
using Azure.Storage.Blobs;
using DataAccess.Configurations;
using DataAccess.Factories;
using DataAccess.Providers;
using Domain.Settings;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriHelper = Domain.UriHelper;

namespace DataAccess.Tests
{
    public abstract class BaseRepositoryTests
    {
        protected readonly IBlobClientOptionsFactory BlobClientOptionsFactory;
        protected readonly IBlobContainerFactory BlobContainerFactory;
        protected readonly IBlobStorageConfigurationProvider BlobStorageConfigurationProvider;
        protected readonly IStorageAccountConfiguration StorageAccountConfiguration;

        protected const string BlobEndpointUri = "http://127.0.0.1:10000/devstoreaccount1";
        protected const string ContainerName = "integration-test";
        protected const string KeyUri = "http://127.0.0.1/mykey";
        protected const string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

        public BaseRepositoryTests()
        {
            var keyUri = new Uri(KeyUri);
            var blobStorageConfiguration = new BlobStorageConfiguration(
              blobEndpointUri: new Uri(BlobEndpointUri),
              containerName: ContainerName,
              keyUri);

            var mockBlobStorageConfigurationProvider = new Mock<IBlobStorageConfigurationProvider>();
            mockBlobStorageConfigurationProvider
                .Setup(x => x.Get())
                .Returns(blobStorageConfiguration);
            var mockBlobStorageSettings = new Mock<IOptions<BlobStorageSettings>>();
            mockBlobStorageSettings.Setup(x => x.Value)
                .Returns(new BlobStorageSettings { DisableEncryptionOnLocal = true });

            BlobStorageConfigurationProvider = mockBlobStorageConfigurationProvider.Object;
            BlobClientOptionsFactory = UriHelper.IsKeyVault(keyUri)
                ? new BlobClientOptionsFactory()
                : new LocalBlobClientOptionFactory(mockBlobStorageSettings.Object);

            var Dict = new Dictionary<string, string>
            {
                { "ConnectionStrings:SystemStorage", ConnectionString },
            };

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(Dict).Build();
            BlobContainerFactory = new LocalBlobContainerFactory(configuration);

            StorageAccountConfiguration = new StorageAccountConfiguration(ConnectionString);
        }

        protected async Task<BlobContainerClient> CreateBlobContainerClientAsync()
        {
            var blobStorageConfiguration = BlobStorageConfigurationProvider.Get();
            var options = BlobClientOptionsFactory.Create(blobStorageConfiguration.KeyUri);

            var containerClient = new BlobContainerClient(ConnectionString, ContainerName, options);

            if (!await containerClient.ExistsAsync()) await containerClient.CreateAsync();

            return containerClient;
        }
    }
}
