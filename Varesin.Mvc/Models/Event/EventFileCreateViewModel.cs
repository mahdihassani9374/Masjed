using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varesin.Domain.Enumeration;

namespace Varesin.Mvc.Models.Event
{
    public class EventFileCreateViewModel
    {
        public string Title { get; set; }
        public IFormFile File { get; set; }
        public FileType FileType { get; set; }
        public int EventId { get; set; }
    }
}
