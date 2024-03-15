namespace Domain.BlobRepositories
{
    public interface IFileBlobRepository
    {
        Task UploadAsync(string filePath, Stream content, bool resetDocumentStreamPosition = true);
    }
}
