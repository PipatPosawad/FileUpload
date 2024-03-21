using Domain.BlobRepositories;
using Domain.Models;
using Domain.QueueRepositories;
using Domain.Services;

namespace Service
{
    /// <summary>
    /// File upload service
    /// </summary>
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileBlobRepository _fileBlobRepository;
        private readonly IMailNotificationQueueRepository _mailNotificationQueueRepository;

        /// <summary>
        /// Initialize a new <see cref="FileUploadService"/> object.
        /// </summary>
        /// <param name="fileBlobRepository"></param>
        /// <param name="mailNotificationQueueRepository"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public FileUploadService(IFileBlobRepository fileBlobRepository,
            IMailNotificationQueueRepository mailNotificationQueueRepository)
        {
            _fileBlobRepository = fileBlobRepository ?? throw new ArgumentNullException(nameof(fileBlobRepository));
            _mailNotificationQueueRepository = mailNotificationQueueRepository ?? throw new ArgumentNullException(nameof(mailNotificationQueueRepository));
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

            var message = new Domain.Messages.MailNotificationMessage()
            {
                FileId = fileModel.Id
            };
            await _mailNotificationQueueRepository.SendMessageAsync(message);

            return fileModel;
        }
    }
}
