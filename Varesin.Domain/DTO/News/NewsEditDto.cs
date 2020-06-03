using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.News
{
    public class NewsEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PrimaryPicture { get; set; }
        public NewsType? Type { get; set; }
    }
}
