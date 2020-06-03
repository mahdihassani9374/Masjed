using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varesin.Mvc.Models.Report
{
    public class ReportUserSearchViewModel
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
        public int? WorkingGroupId { get; set; }
    }
}
