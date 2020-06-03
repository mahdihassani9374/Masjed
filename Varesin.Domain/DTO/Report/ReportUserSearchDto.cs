using System;
using System.Collections.Generic;
using System.Text;

namespace Varesin.Domain.DTO.Report
{
    public class ReportUserSearchDto
    {
        public string Title { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
        public int? WorkingGroupId { get; set; }
    }
}
