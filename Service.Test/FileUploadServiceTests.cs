using Domain.BlobRepositories;
using Domain.Messages;
using Domain.QueueRepositories;
using Moq;

namespace Service.Test
{
    [TestClass]
    public class FileUploadServiceTests
    {
        private Mock<IMailNotificationQueueRepository> _mockQueueRepository;
        private Mock<IFileBlobRepository> _mockFileBlobRepository;

        private FileUploadService _service;

        [TestInitialize]
        public void Initialize()
        {
            _mockQueueRepository = new Mock<IMailNotificationQueueRepository>();
            _mockFileBlobRepository = new Mock<IFileBlobRepository>();

            _service = new FileUploadService(_mockFileBlobRepository.Object,
                _mockQueueRepository.Object);
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

            _mockQueueRepository.Verify(x => x.SendMessageAsync(It.IsAny<MailNotificationMessage>()), Times.Once);
        }
    }
}