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
        /// Uploads file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<FileModel> UploadAsync(string name, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
