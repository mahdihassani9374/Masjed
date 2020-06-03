using Microsoft.AspNetCore.Http;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.News
{
    public class NewsCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile PrimaryPicture { get; set; }
        public NewsType? Type { get; set; }
    }
}
