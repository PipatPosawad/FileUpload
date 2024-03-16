using Azure.Storage.Blobs;
using DataAccess.BlobRepositories;
using DataAccess.Configurations;
using DataAccess.Factories;
using DataAccess.Providers;
using Microsoft.Extensions.Logging;
using Moq;

namespace DataAccess.Tests
{
    [TestClass]
    public class FileBlobRepositoryTests
    {
        private Mock<IBlobClientOptionsFactory> _mockBlobClientOptionsFactory;
        private Mock<IBlobStorageConfigurationProvider> _mockBlobStorageConfigurationProvider;
        private Mock<ILogger> _mockLogger;
        private Mock<IBlobContainerFactory> _mockBlobContainerFactory;

        private FileBlobRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _mockBlobClientOptionsFactory = new Mock<IBlobClientOptionsFactory>();
            _mockBlobStorageConfigurationProvider = new Mock<IBlobStorageConfigurationProvider>();
            _mockLogger = new Mock<ILogger>();
            _mockBlobContainerFactory = new Mock<IBlobContainerFactory>();

            _repository = new FileBlobRepository(
                _mockBlobClientOptionsFactory.Object,
                _mockBlobStorageConfigurationProvider.Object,
                _mockLogger.Object,
                _mockBlobContainerFactory.Object);
        }

        [TestMethod]
        public async Task UploadAsync_ReturnsNothing_WhenUploadFileIsSuccessful()
        {
            // Arrange
            var filePath = "mock-path";
            using var content = new MemoryStream();

            var keyUri = new Uri("https://localhost/key");
            var mockBlobStorageConfiguration = new Mock<BlobStorageConfiguration>();
            var mockBlobContainerClient = new Mock<BlobContainerClient>();
            var mockBlobClient = new Mock<BlobClient>();

            mockBlobStorageConfiguration.Setup(x => x.KeyUri).Returns(keyUri);

            _mockBlobStorageConfigurationProvider.Setup(x => x.Get())
                .Returns(mockBlobStorageConfiguration.Object);

            _mockBlobContainerFactory.Setup(x => x.Get(It.IsAny<Uri>(), It.IsAny<BlobClientOptions>()))
                .Returns(mockBlobContainerClient.Object);

            mockBlobContainerClient.Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(mockBlobClient.Object);

            // Act
            await _repository.UploadAsync(filePath, content);

            // Assert
            mockBlobClient.Verify(x => x.UploadAsync(content), Times.Once);
        }
    }
}