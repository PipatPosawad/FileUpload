using Castle.Core.Logging;
using Domain.Messages;
using Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Service;
using System;
using System.Threading.Tasks;

namespace FunctionApp.Tests
{
    [TestClass]
    public class MailNotificationFunctionTests
    {
        private Mock<ILogger<MailNotificationFunction>> _mockLogger;
        private Mock<IMailNotificationService> _mockMailService;

        private MailNotificationFunction _function;

        [TestInitialize]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger<MailNotificationFunction>>();
            _mockMailService = new Mock<IMailNotificationService>();

            _function = new MailNotificationFunction(_mockLogger.Object, _mockMailService.Object);
        }

        [TestMethod]
        public async Task RunAsync_SendsMail_WhenOperationIsSuccessful()
        {
            // Arrange
            var message = new MailNotificationMessage()
            {
                FileId = Guid.NewGuid(),
            };

            // Act
            await _function.RunAsync(message);

            // Assert
            _mockMailService.Verify(x => x.SendMailAsync(message.FileId), Times.Once());
        }
    }
}