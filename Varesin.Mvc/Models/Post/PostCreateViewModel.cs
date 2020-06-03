using Microsoft.AspNetCore.Http;

namespace Varesin.Mvc.Models.Post
{
    public class PostCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile PrimaryPicture { get; set; }
    }
}
