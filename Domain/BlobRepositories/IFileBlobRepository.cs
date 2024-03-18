using Domain.Models;

namespace Domain.BlobRepositories
{
    public interface IFileBlobRepository
    {
        Task UploadAsync(FileModel file, Stream content, bool resetStreamPosition = true);

        Task<FileContentModel> OpenReadStreamAsync(Guid id);
    }
}
