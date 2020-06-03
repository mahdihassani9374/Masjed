using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.Report;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal? CostEstimation { get; set; }
        public decimal? CostCollected { get; set; }
        public ProjectState? State { get; set; }
        public virtual ProjectTypeDto Type { get; set; }
        public int? TypeId { get; set; }
        public int? ReportId { get; set; }
        public virtual ReportDto Report { get; set; }
    }
}
