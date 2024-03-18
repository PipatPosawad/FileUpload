using Domain.BlobRepositories;
using Domain.Models;
using Domain.Services;

namespace Service
{
    /// <summary>
    /// File upload service
    /// </summary>
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileBlobRepository _fileBlobRepository;

        /// <summary>
        /// Initialize a new <see cref="FileUploadService"/> object.
        /// </summary>
        /// <param name="fileBlobRepository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileUploadService(IFileBlobRepository fileBlobRepository)
        {
            _fileBlobRepository = fileBlobRepository ?? throw new ArgumentNullException(nameof(fileBlobRepository));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FileContentModel> DownloadAsync(Guid id)
        {
            return await _fileBlobRepository.OpenReadStreamAsync(id);
        }

        /// <summary>
        /// Uploads file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<FileModel> UploadAsync(string name, string contentType, Stream stream)
        {
            var fileModel = new FileModel
            {
                Id = Guid.NewGuid(),
                Name = name,
                SizeInBytes = stream.Length,
                ContentType = contentType
            };

            await _fileBlobRepository.UploadAsync(fileModel, stream);

            return fileModel;
        }
    }
}
