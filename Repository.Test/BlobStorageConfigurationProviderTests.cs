using DataAccess.Configurations;
using DataAccess.Providers;
using Moq;

namespace DataAccess.Tests
{
    [TestClass]
    public class BlobStorageConfigurationProviderTests
    {
        private Mock<IStorageAccountConfiguration> _mockStorageAccountConfiguration;
        private Mock<IKeyVaultConfiguration> _mockKeyVaultConfiguration;

        private BlobStorageConfigurationProvider _configurationProvider;

        [TestInitialize]
        public void Initialize()
        {
            _mockStorageAccountConfiguration = new Mock<IStorageAccountConfiguration>();
            _mockKeyVaultConfiguration = new Mock<IKeyVaultConfiguration>();

            _configurationProvider = new BlobStorageConfigurationProvider(
                _mockStorageAccountConfiguration.Object,
                _mockKeyVaultConfiguration.Object
                );
        }

        [TestMethod]
        public void Get_ReturnsBlobStorageConfiguration_WhenInputIsValid()
        {
            // Arrange
            _mockStorageAccountConfiguration.Setup(x => x.ConnectionString)
                .Returns("mock-connection-string");

            var uri = new Uri("https://localhost");
            _mockKeyVaultConfiguration.Setup(x => x.Uri)
                .Returns(uri);

            // Act
            var actual = _configurationProvider.Get();

            // Assert
            Assert.IsNotNull(actual);
        }
    }
}
