using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Mvc.Models.WorkingGroup;

namespace Varesin.Mvc.Models.Report
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public int? WorkingGroupId { get; set; }
        public WorkingGroupViewModel WorkingGroup { get; set; }
    }
}
