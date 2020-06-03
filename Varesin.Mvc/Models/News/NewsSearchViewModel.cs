using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.News
{
    public class NewsSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public NewsType? Type { get; set; }
    }
}
