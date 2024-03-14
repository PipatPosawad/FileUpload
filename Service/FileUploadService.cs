using Domain.Services;

namespace Service
{
    public class FileUploadService : IFileUploadService
    {
        public Task<Domain.File> UploadAsync(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
