using Microsoft.AspNetCore.Http;

namespace Varesin.Mvc.Models.Report
{
    public class ReportCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? WorkingGroupId { get; set; }
        public IFormFile PrimaryPicture { get; set; }
    }
}
