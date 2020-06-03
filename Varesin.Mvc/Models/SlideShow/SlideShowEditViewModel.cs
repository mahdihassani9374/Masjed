using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Varesin.Mvc.Models.SlideShow
{
    public class SlideShowEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public string Link { get; set; }
    }
}
