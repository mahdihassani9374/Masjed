using Microsoft.AspNetCore.Http;

namespace Varesin.Mvc.Models.SlideShow
{
    public class SlideShowCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string Link { get; set; }
    }
}
