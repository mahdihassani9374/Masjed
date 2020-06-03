namespace Varesin.Mvc.Models.Post
{
    public class PostSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
