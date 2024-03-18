using Domain.BlobRepositories;
using Moq;

namespace Service.Test
{
    [TestClass]
    public class FileUploadServiceTests
    {
        private Mock<IFileBlobRepository> _mockFileBlobRepository;

        private FileUploadService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockFileBlobRepository = new Mock<IFileBlobRepository>();

            _service = new FileUploadService(_mockFileBlobRepository.Object);
        }

        [TestMethod]
        public async Task UploadAsync_ReturnsFileModel_WhenUploadingIsSuccessful()
        {
            // Arrange
            var name = "";
            var contentType = "";
            using var stream = new MemoryStream();

            // Act
            var actual = await _service.UploadAsync(name, contentType, stream);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}