using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Text;
using WebApi.Controllers;

namespace WebApi.Tests
{
    [TestClass]
    public class FileControllerTests : BaseControllerTests
    {
        private Mock<IFileUploadService> _mockFileUploadService;

        private FileController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockFileUploadService = new Mock<IFileUploadService>();

            _controller = new FileController(_mockFileUploadService.Object);

            CreateHttpContext(_controller);
        }

        [TestMethod]
        public async Task PostUploadAsync_ReturnsOk_WhenOperationIsSuccessful()
        {
            // Arrange
            var file = GetSampleFile();

            // Act
            var result = await _controller.PostUploadAsync(file) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            _mockFileUploadService.Verify(x => x.UploadAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Stream>()), Times.Once);
        }

        private static IFormFile GetSampleFile()
        {
            var bytes = Encoding.UTF8.GetBytes("example file content");
            var stream = new MemoryStream(bytes);

            var mockFormFile = new Mock<IFormFile>();
            mockFormFile.SetupAllProperties();
            mockFormFile.Setup(x => x.OpenReadStream()).Returns(stream);
            mockFormFile.SetupGet(x => x.Length).Returns(bytes.Length);
            mockFormFile.SetupGet(x => x.FileName).Returns("sample-file.txt");
            mockFormFile.SetupGet(x => x.Name).Returns("Sameple File");

            return mockFormFile.Object;
        }
    }
}