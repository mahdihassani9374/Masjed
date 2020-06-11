using Microsoft.AspNetCore.Http;

namespace Varesin.Mvc.Models.Event
{
    public class EventCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool MultiDay { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public IFormFile PrimaryPicture { get; set; }
    }
}
