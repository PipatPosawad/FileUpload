using Domain.Models;

namespace Domain.Services
{
    /// <summary>
    /// File upload service
    /// </summary>
    public interface IFileUploadService
    {
        /// <summary>
        /// Uploads a file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="contentType"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<FileModel> UploadAsync(string name, string contentType, Stream stream);
    }
}
