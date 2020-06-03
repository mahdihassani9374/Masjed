using System.Collections.Generic;

namespace Varesin.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public int WorkingGroupId { get; set; }
        public virtual WorkingGroup WorkingGroup { get; set; }
        public virtual ICollection<ReportFile> Files { get; set; }
        public virtual Project Project { get; set; }
        public int? ProjectId { get; set; }
    }
}
