using System;
using System.Collections.Generic;
using System.Text;
using Varesin.Domain.Enumeration;

namespace Varesin.Domain.DTO.Project
{
    public class ProjectUserSearchDto
    {
        public string Title { get; set; }
        public ProjectState? State { get; set; }
        public int? TypeId { get; set; }
        public int PageSize { get; set; } = 12;
        public int PageNumber { get; set; } = 1;
    }
}
