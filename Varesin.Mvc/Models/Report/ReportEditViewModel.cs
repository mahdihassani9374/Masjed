using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varesin.Mvc.Models.Report
{
    public class ReportEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? WorkingGroupId { get; set; }
        public IFormFile PrimaryPicture { get; set; }
    }
}
