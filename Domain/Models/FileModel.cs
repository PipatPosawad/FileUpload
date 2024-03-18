namespace Domain.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public long SizeInBytes { get; set; }
    }
}
