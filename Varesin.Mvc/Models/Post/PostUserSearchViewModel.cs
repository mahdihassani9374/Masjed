namespace Varesin.Mvc.Models.Post
{
    public class PostUserSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
    }
}
