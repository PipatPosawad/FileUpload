using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DataAccess.BlobRepositories;
using DataAccess.Configurations;
using DataAccess.Factories;
using DataAccess.Providers;
using Domain;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Mime;
using System.Text;

namespace DataAccess.Tests
{
    [TestClass]
    [TestCategory("IntegrationTest")]
    public class FileBlobRepositoryTests : BaseRepositoryTests
    {
        private Mock<ILogger> _mockLogger;

        private FileBlobRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger>();

            _repository = new FileBlobRepository(
                BlobClientOptionsFactory,
                BlobStorageConfigurationProvider,
                _mockLogger.Object,
                BlobContainerFactory);
        }

        [TestMethod]
        public async Task UploadAsync_ReturnsNothing_WhenUploadFileIsSuccessful()
        {
            // Arrange
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes("hello world"));
            var fileName = "Test.pdf";
            var contentType = "application/pdf";
            var fileModel = new FileModel()
            {
                Name = fileName,
                ContentType = contentType,
                Id = Guid.NewGuid(),
                SizeInBytes = ms.Length
            };
            var blobClient = await GetBlobClientAsync(fileModel.Id.ToString());
            await blobClient.DeleteIfExistsAsync();
            
            var metadata = new Dictionary<string, string>()
            {
                { BlobStorageMetadataKeys.OriginalFileName, fileName }
            };
            using var content = new MemoryStream(Encoding.UTF8.GetBytes("hello world"));

            // Act
            await _repository.UploadAsync(fileModel, content);

            // Assert
            var fileExists = await blobClient.ExistsAsync();
            Assert.IsTrue(fileExists);

            BlobProperties properties = await blobClient.GetPropertiesAsync();
            Assert.IsTrue(fileExists);
            Assert.AreEqual(contentType, properties.ContentType);
            Assert.AreEqual(metadata[BlobStorageMetadataKeys.OriginalFileName], properties.Metadata[BlobStorageMetadataKeys.OriginalFileName]);
        }

        private async Task<BlobClient> GetBlobClientAsync(string filePath)
        {
            var fullPath = FormattableString.Invariant($"{_repository.SubContainerName}/{filePath}");
            var blobContainer = await CreateBlobContainerClientAsync();
            var blobClient = blobContainer.GetBlobClient(fullPath);

            return blobClient;
        }
    }
}