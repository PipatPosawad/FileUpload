using DataAccess.Factories;

namespace DataAccess.Tests
{
    [TestClass]
    public class BlobClientOptionsFactoryTests
    {
        private BlobClientOptionsFactory _blobClientOptionsFactory;

        [TestInitialize]
        public void Initialize()
        {
            _blobClientOptionsFactory = new BlobClientOptionsFactory();
        }

        [TestMethod]
        public void Create_ReturnsBlobClientOptions_WhenOperationIsSuccessful()
        {
            // Arrange
            var keyUri = new Uri("https://localhost");

            // Act
            var actual = _blobClientOptionsFactory.Create(keyUri);

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
