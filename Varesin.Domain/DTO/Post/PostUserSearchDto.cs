namespace Varesin.Domain.DTO.Post
{
    public class PostUserSearchDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
    }
}
