using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Project
{
    public class ProjectCreateViewModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public decimal? CostEstimation { get; set; }
        public int? TypeId { get; set; }
        public ProjectState? State { get; set; }
    }
}
