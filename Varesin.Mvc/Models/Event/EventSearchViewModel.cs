namespace Varesin.Mvc.Models.Event
{
    public class EventSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
