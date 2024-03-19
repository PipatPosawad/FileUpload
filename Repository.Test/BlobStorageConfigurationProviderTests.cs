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
                .Returns("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");

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
