using Varesin.Domain.Enumeration;

namespace Varesin.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal? CostEstimation { get; set; }
        public ProjectState State { get; set; }
        public virtual ProjectType Type { get; set; }
        public int TypeId { get; set; }
        public decimal? CostCollected { get; set; }

        public virtual Report Report { get; set; }
        public int? ReportId { get; set; }
    }
}
