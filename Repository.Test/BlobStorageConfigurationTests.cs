using DataAccess.Configurations;

namespace DataAccess.Tests
{
    [TestClass]
    public class BlobStorageConfigurationTests
    {
        [TestMethod]
        public void Constructor_ReturnsConfiguration_WhenConfigurationIsValid()
        {
            // Arrange
            var blobEndpointUri = new Uri("https://localhost");
            var containerName = "mock-container-name";
            var keyUri = new Uri("https://localhost/key");

            // Act
            var actual = new BlobStorageConfiguration(blobEndpointUri,
                containerName,
                keyUri);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
