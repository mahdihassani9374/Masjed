using Varesin.Domain.Enumeration;

namespace Varesin.Domain.Entities
{
    public class PostFile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public FileType Type { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public int CountDownload { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
