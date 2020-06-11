using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varesin.Mvc.Models.Event
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrimaryPicture { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Date { get; set; }
        public string Time { get; set; }
        public bool MultiDay { get; set; }
    }
}
