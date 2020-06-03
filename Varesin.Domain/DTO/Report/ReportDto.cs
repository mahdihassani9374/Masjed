using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.DTO.WorkingGroup;

namespace Varesin.Domain.DTO.Report
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public int? WorkingGroupId { get; set; }
        public virtual WorkingGroupDto WorkingGroup { get; set; }
    }
}
