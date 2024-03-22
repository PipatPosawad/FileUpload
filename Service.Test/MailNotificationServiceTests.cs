using DataAccess.BlobRepositories;
using Domain;
using Domain.BlobRepositories;
using Domain.Constants;
using Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests
{
    [TestClass]
    public class MailNotificationServiceTests
    {
        private Mock<IFileBlobRepository> _mockFileBlobRepository;
        private Mock<IOptions<MailNotificationSettings>> _mockMailNotificationSettings;
        private Mock<ILogger> _mockLogger;
        private Mock<ISmtpClientFactory> _mockSmtpClientFactory;

        private MailNotificationService _service;

        [TestInitialize]
        public void Initialize()
        {
            var settings = new MailNotificationSettings()
            {
                FromAddress = "mock.from@mail.com",
                SmtpPort = 0,
                SmtpServer = "localhost",
                Subject = "mock-subject",
                ToAddress = "mock.to@mail.com"
            };

            _mockFileBlobRepository = new Mock<IFileBlobRepository>();
            _mockMailNotificationSettings = new Mock<IOptions<MailNotificationSettings>>();
            _mockSmtpClientFactory = new Mock<ISmtpClientFactory>();
            _mockLogger = new Mock<ILogger>();

            _mockMailNotificationSettings.Setup(x => x.Value)
                .Returns(settings);

            _service = new MailNotificationService(_mockFileBlobRepository.Object,
                _mockMailNotificationSettings.Object,
                _mockLogger.Object,
                _mockSmtpClientFactory.Object);
        }

        [TestMethod]
        public async Task SendMailAsync_SendsMail_WhenOperationIsSuccesful()
        {
            // Arrange
            var fileId = Guid.NewGuid();
            var mockSmtpClientWrapper = new Mock<ISmtpClient>();
            var metadata = new Dictionary<string, string>()
            {
                { BlobStorageMetadataKeys.OriginalFileName, "mock-file-name" }
            };

            _mockFileBlobRepository.Setup(x => x.GetFileMetadataAsync(It.IsAny<Guid>()))
                .ReturnsAsync(metadata);

            _mockSmtpClientFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(mockSmtpClientWrapper.Object);

            // Act
            await _service.SendMailAsync(fileId);

            // Assert
            mockSmtpClientWrapper.Verify(x => x.Send(It.IsAny<MailMessage>()), Times.Once);
        }
    }
}
