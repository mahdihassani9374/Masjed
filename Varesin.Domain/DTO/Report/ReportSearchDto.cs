using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Report
{
    public class ReportSearchDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; } 
        public int PageNumber { get; set; } 
        public int? WorkingGroupId { get; set; }
    }
}
