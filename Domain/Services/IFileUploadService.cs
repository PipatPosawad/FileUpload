namespace Domain.Services
{
    public interface IFileUploadService
    {
        Task<File> UploadAsync(Stream stream);
    }
}
