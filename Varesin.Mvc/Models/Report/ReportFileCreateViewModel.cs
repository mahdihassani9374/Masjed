using Microsoft.AspNetCore.Http;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Report
{
    public class ReportFileCreateViewModel
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public FileType FileType { get; set; }
        public int ReportId { get; set; }
    }
}
