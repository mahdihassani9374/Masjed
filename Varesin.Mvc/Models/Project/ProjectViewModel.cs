using Varesin.Domain.Enumeration;
using Varesin.Mvc.Models.Report;

namespace Varesin.Mvc.Models.Project
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal? CostEstimation { get; set; }
        public decimal? CostCollected { get; set; }
        public ProjectState? State { get; set; }
        public virtual ProjectTypeViewModel Type { get; set; }
        public int? TypeId { get; set; }
        public int? ReportId { get; set; }
        public virtual ReportViewModel Report { get; set; }
        public string RelativeImage { get; set; }
    }
}
